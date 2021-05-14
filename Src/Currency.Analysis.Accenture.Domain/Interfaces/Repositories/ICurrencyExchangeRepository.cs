
using Currency.Analysis.Accenture.Domain.Entities;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Interfaces.Repositories
{
    public interface ICurrencyExchangeRepository
    {
       Task Register(Currencies data);
    }
}
