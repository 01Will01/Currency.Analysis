
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Enums;
using Currency.Analysis.Accenture.Domain.Interfaces.Repositories;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Services
{
    public class ExchangeRateService: IExchangeRateService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public ExchangeRateService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }
        public async Task Register(Currencies data)
        {
            await _currencyExchangeRepository.Register(data);
        }

        public string GetType(int? type)
        {

            return type switch
            {
                (int)CurrencyTypes.USD => "price_usd",
                (int)CurrencyTypes.BTC => "price_btc",
                (int)CurrencyTypes.ETH => "price_eth",
                _ => ""
            };
        }

        public decimal GetExchangeRate(int? typeExchangeRate, int? typeApplied, MessariExchangeRateDTO messari)
        {
            decimal value = 0;
            string symbol = "";

            if (typeApplied == (int)CurrencyTypes.BTC) { symbol = "BTC"; }
            else if (typeApplied == (int)CurrencyTypes.ETH) { symbol = "ETH"; }

            messari.Data.ForEach(item =>
            {
                if(value > 0) { return; }

                if (typeExchangeRate == (int)CurrencyTypes.USD && symbol == item.Symbol) { value = item.Metrics.market_data.price_usd; }
                else if (typeExchangeRate == (int)CurrencyTypes.BTC && symbol == item.Symbol) { value = item.Metrics.market_data.price_btc; }
                else if (typeExchangeRate == (int)CurrencyTypes.ETH && symbol == item.Symbol) { value = item.Metrics.market_data.price_eth; }
            });

            return value;
        }

        public string GetCurrencyName(int? type)
        {

            return type switch
            {
                (int)CurrencyTypes.USD => "Dolar dos Estados Unidos",
                (int)CurrencyTypes.BTC => "BitCoin",
                (int)CurrencyTypes.ETH => "Ethereum",
                _ => ""
            };
        }
    }
}
