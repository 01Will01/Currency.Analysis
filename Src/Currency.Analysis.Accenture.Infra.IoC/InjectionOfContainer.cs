using Currency.Analysis.Accenture.Domain.CommandHandlers;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Domain.Interfaces.Queries;
using Currency.Analysis.Accenture.Domain.Interfaces.Repositories;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using Currency.Analysis.Accenture.Domain.Services;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Currency.Analysis.Accenture.Infra.Data.Queries;
using Currency.Analysis.Accenture.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Currency.Analysis.Accenture.Infra.IoC
{
    public class InjectionOfContainer
    {

        public static void ConfigureInjection(IServiceCollection services)
        {

            // Contexto dos dados
            services.AddScoped<CAASystemDataContext, CAASystemDataContext>();

            // Commandos de manipulação
            services.AddScoped<ExchangeRateCommandHandler, ExchangeRateCommandHandler>();

            // Services
            services.AddScoped<IExchangeRateService, ExchangeRateService>();

            // Consultas
            services.AddScoped<IExchangeRateQuery, ExchangeRateQuery>();
            services.AddScoped<ICurrencyExchangeQuery, CurrencyExchangeQuery>();

            // Repositorios
            services.AddScoped<ICurrencyExchangeRepository, CurrencyExchangeRepository>();

        }
    }
}
