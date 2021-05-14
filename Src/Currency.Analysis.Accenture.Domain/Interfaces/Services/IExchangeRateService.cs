
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Interfaces.Services
{
    public interface IExchangeRateService
    {
        string GetType(int? type);

        decimal GetExchangeRate(int? typeExchangeRate, int? typeApplied, MessariExchangeRateDTO messari);

        string GetCurrencyName(int? type);
        Task Register(Currencies data);
    }
}
