
namespace Currency.Analysis.Accenture.Shared
{
    public class Settings
    {
        public const string COINLORE_EXCHANGE_RATE_URL = "https://api.coinlore.net/api/exchange/?id=5";

        public const string COINLORE_CURRENCIES_URL = "https://api.coinlore.net/api/tickers/";

        public const string COINLORE_EXCHANGE_KEY = "ExchangeRate";

        public const string COINLORE_CURRENCY_KEY = "Currency";

        public const string MESSARI_URL = "https://data.messari.io/api/v2/assets?fields=id,slug,symbol,metrics/market_data/";
    }
}
