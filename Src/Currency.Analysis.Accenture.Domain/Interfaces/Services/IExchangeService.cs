
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Interfaces.Services
{
    public interface IExchangeService
    {
        decimal GetExchangeRate(string typeExchangeRate, string typeApplied, CoinloreExchangeRateDTO coinlore);
        Task Register(Currencies data);
    }
}
