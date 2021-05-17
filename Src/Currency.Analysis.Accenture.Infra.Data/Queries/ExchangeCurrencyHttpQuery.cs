using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Shared;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Infra.Data.Queries
{
    public class ExchangeCurrencyHttpQuery : IExchangeCurrencyHttpQuery
    {

        private readonly IMemoryCache _memoryCache;
        public ExchangeCurrencyHttpQuery(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<CoinloreExchangeRateDTO> GetDataExchangeRate(string url)
        {

            if (_memoryCache.TryGetValue(Settings.COINLORE_EXCHANGE_KEY, out CoinloreExchangeRateDTO data))
            {
                return data;
            }

            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(url);
                var responseData = await response.Content.ReadAsStringAsync();

                data = JsonSerializer.Deserialize<CoinloreExchangeRateDTO>(responseData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                _memoryCache.Set(
                    Settings.COINLORE_EXCHANGE_KEY,
                    data,
                    new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1800),
                        SlidingExpiration = TimeSpan.FromSeconds(150)
                    });

                return data;
            }
        }


        public async Task<CoinloreCurrenciesDTO> GetDataCurrencies(string url)
        {

            if (_memoryCache.TryGetValue(Settings.COINLORE_CURRENCY_KEY, out CoinloreCurrenciesDTO data))
            {
                return data;
            }

            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(url);
                var responseData = await response.Content.ReadAsStringAsync();

                data = JsonSerializer.Deserialize<CoinloreCurrenciesDTO>(responseData, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                _memoryCache.Set(
                    Settings.COINLORE_CURRENCY_KEY,
                    data,
                    new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1800),
                        SlidingExpiration = TimeSpan.FromSeconds(150)
                    });

                return data;
            }
        }


        public List<CurrencyNameDTO> FilterApplied(List<CurrencyNameDTO> data, CoinloreCurrenciesDTO currencies, CoinloreExchangeRateDTO exchanges)
        {
            var initial = new List<CurrencyNameDTO>();
            var result = new List<CurrencyNameDTO>();

            exchanges.Pairs.ForEach(pair =>
            {
                if (!initial.Any(currency => currency.Symbol == pair.Base))
                {
                    if (!(pair.Base is null) && InvalidSymbols(pair.Base)) { initial.Add(new CurrencyNameDTO() { Symbol = pair.Base }); }
                }
            });

            result = ValidationApplied(initial, data, exchanges);
            result.ForEach(data => { data = SetCurrencyName(data, currencies); });

            return result;
        }

        public List<CurrencyNameDTO> ValidationApplied(List<CurrencyNameDTO> applied, List<CurrencyNameDTO> replacement, CoinloreExchangeRateDTO exchanges)
        {
            var result = new List<CurrencyNameDTO>();
            bool status = true;
            string symbol = "";

            applied.ForEach(applied =>
            {
                replacement.ForEach(replacement =>
                {
                    exchanges.Pairs.ForEach(pair =>
                    {
                        // Validação para verificar se a moédade de aplicação consta para todas as moédas de troco
                        // Porém, não a combinação para todas.
                        // Logo resulta em lista vazia
                        /*
                        *   if
                        *   (pair.Quote == replacement.Symbol && pair.Base == applied.Symbol && status)
                        *   { symbol = applied.Symbol; }
                        *   else
                        *   { status = false; }
                        */

                        // Por isso, filtro alternativo
                        if
                        (pair.Quote == replacement.Symbol && pair.Base == applied.Symbol)
                        { symbol = applied.Symbol; }

                    });
                });

                if (!result.Any(currency => currency.Symbol == symbol) && status)
                {
                    result.Add(new CurrencyNameDTO() { Symbol = symbol });
                    status = true;
                }
            });

            return result;
        }


        public List<CurrencyNameDTO> FilterReplacement(CoinloreCurrenciesDTO currencies, CoinloreExchangeRateDTO exchanges)
        {
            var result = new List<CurrencyNameDTO>();

            exchanges.Pairs.ForEach(pair =>
            {
                if (!result.Any(currency => currency.Symbol == pair.Quote))
                {
                    if (!(pair.Quote is null)) { result.Add(new CurrencyNameDTO() { Symbol = pair.Quote }); }
                }
            });

            result.ForEach(data => { data = SetCurrencyName(data, currencies); });

            return result;
        }

        public CurrencyNameDTO SetCurrencyName(CurrencyNameDTO data, CoinloreCurrenciesDTO currencies)
        {
            foreach (var currency in currencies.Data)
            {
                if (!(data.Name is null)) { break; }

                if (data.Symbol == currency.Symbol) { data.Name = currency.Name; }

                if (ValidSymbols(data.Symbol)) { data.Name = ReservedSymbols(data.Symbol); }
            }
            return data;
        }


        public async Task<(List<CurrencyNameDTO>, List<CurrencyNameDTO>)> GetListCurrencyNames(string urlCurrencies, string urlExchangeRate)
        {
            CoinloreCurrenciesDTO currencyObject = await GetDataCurrencies(urlCurrencies);
            CoinloreExchangeRateDTO exchangeRateObject = await GetDataExchangeRate(urlExchangeRate);

            if (exchangeRateObject.Pairs.Count > 0)
            {
                var currenciesReplacement = FilterReplacement(currencyObject, exchangeRateObject);

                var currenciesApplied = FilterApplied(currenciesReplacement, currencyObject, exchangeRateObject);

                return (currenciesApplied, currenciesReplacement);
            }
            return (null, null);
        }

        public MessariExchangeRateDTO Get(string url, string type)
        {
            var URLToken = new RestClient(url + type);
            URLToken.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var request = new RestRequest(Method.GET);

            string ResponseBody = URLToken.Execute(request).Content;

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            MessariExchangeRateDTO data = JsonSerializer.Deserialize<MessariExchangeRateDTO>(ResponseBody, options);

            return data;
        }

        public bool InvalidSymbols(string value)
        {
            return value switch
            {
                "USDT" => false,
                "NKN" => false,
                "ETHUP" => false,
                "ETHDOWN" => false,
                "PSG" => false,
                "NEAR" => false,
                "CTSI" => false,
                "ALGO" => false,
                "CRV" => false,
                "COTI" => false,
                "RUNE" => false,
                "HBAR" => false,
                "GRT" => false,
                "SXP" => false,
                "LUNA" => false,
                "NMR" => false,
                "ONE" => false,
                _ => true
            };
        }

        public bool ValidSymbols(string value)
        {
            return value switch
            {
                "DAI" => true,
                _ => false
            };
        }


        public string ReservedSymbols(string value)
        {
            return value switch
            {
                "DAI" => "MakerDao",
                _ => "Indefinido"
            };
        }
    }
}
