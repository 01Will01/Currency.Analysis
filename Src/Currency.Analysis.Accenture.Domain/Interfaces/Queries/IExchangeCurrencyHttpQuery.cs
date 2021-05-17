
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Interfaces
{
    public interface IExchangeCurrencyHttpQuery
    {
        Task<CoinloreExchangeRateDTO> GetDataExchangeRate(string url);

        Task<CoinloreCurrenciesDTO> GetDataCurrencies(string url);

        MessariExchangeRateDTO Get(string url, string type);

        Task<(List<CurrencyNameDTO>, List<CurrencyNameDTO>)> GetListCurrencyNames(string urlCurrencies, string urlExchangeRate);

    }
}
