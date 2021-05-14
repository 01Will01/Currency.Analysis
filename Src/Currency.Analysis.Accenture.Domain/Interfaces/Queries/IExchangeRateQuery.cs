
using Currency.Analysis.Accenture.Domain.DTOs;

namespace Currency.Analysis.Accenture.Domain.Interfaces
{
    public interface IExchangeRateQuery
    {
        ExchangeRateDTO Get(string url, string token, string cunrrencies);

        MessariExchangeRateDTO GetOfficial(string url, string type);
    }
}
