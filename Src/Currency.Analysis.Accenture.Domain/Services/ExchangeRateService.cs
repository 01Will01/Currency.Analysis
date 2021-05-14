
using Currency.Analysis.Accenture.Domain.Enums;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;

namespace Currency.Analysis.Accenture.Domain.Services
{
    public class ExchangeRateService: IExchangeRateService
    {
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
    }
}
