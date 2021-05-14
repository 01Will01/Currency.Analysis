using Currency.Analysis.Accenture.Domain.CommandHandlers;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using Currency.Analysis.Accenture.Domain.Services;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Currency.Analysis.Accenture.Infra.Data.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Currency.Analysis.Accenture.Infra.IoC
{
    public class InjectionOfContainer
    {

        public static void ConfigureInjection(IServiceCollection services)
        {

            // Contexto dos dados
            services.AddScoped<CAASystemDataContext, CAASystemDataContext>();

            // CommandHandlers
            services.AddScoped<ExchangeRateCommandHandler, ExchangeRateCommandHandler>();

            // Services
            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            // Consultas
            services.AddScoped<IExchangeRateQuery, ExchangeRateQuery>();

        }
    }
}
