using Currency.Analysis.Accenture.Infra.Data.Queries;
using Currency.Analysis.Accenture.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Currency.Analysis.Accenture.Test
{
    [TestClass]
    public class ExchangeRateTest
    {
        [TestMethod]
        public void get()
        {
            ExchangeRateQuery _exchangeRate = new ExchangeRateQuery();

            _exchangeRate.Get(Settings.URLIntegration, Settings.URLIntegration, Settings.OptionsIntegration);
        }
    }
}
