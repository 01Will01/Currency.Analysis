
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Enums;
using Currency.Analysis.Accenture.Domain.Interfaces.Repositories;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly ICurrencyExchangeRepository _currencyExchangeRepository;

        public ExchangeService(ICurrencyExchangeRepository currencyExchangeRepository)
        {
            _currencyExchangeRepository = currencyExchangeRepository;
        }

        public async Task Register(Currencies data)
        {
            await _currencyExchangeRepository.Register(data);
        }

        public decimal GetExchangeRate(string typeExchangeRate, string typeApplied, CoinloreExchangeRateDTO coinlore)
        {
            decimal value = 0;
            coinlore.Pairs.ForEach(pair => {
                if (pair.Base == typeApplied && pair.Quote == typeExchangeRate) { value = pair.Price; }
            });
            return value;
        }
    }
}
