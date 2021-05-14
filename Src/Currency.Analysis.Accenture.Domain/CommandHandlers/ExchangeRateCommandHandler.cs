using Currency.Analysis.Accenture.Domain.Commands;
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using Currency.Analysis.Accenture.Shared;
using Currency.Analysis.Accenture.Shared.Commands;
using Currency.Analysis.Accenture.Shared.Handlers.Interfaces;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.CommandHandlers
{

    public class ExchangeRateCommandHandler: IHandler<ExchangeResgisterCommand>
    {
        private readonly IExchangeRateQuery _exchangeRateQuery;
        private readonly IExchangeRateService _exchangeRateService;

        public ExchangeRateCommandHandler(IExchangeRateQuery exchangeRateQuery, IExchangeRateService exchangeRateService)
        {
            _exchangeRateQuery = exchangeRateQuery;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<GenericCommandResult> Handle(ExchangeResgisterCommand command)
        {
            command.Validate();

            if (command.Invalid) return new GenericCommandResult(false, "Valores de entrada inválidos", new { });

            string type = _exchangeRateService.GetType(command.Applied);

            MessariExchangeRateDTO data =  _exchangeRateQuery.GetOfficial(Settings.URLIntegrationOfficial, type);

            return new GenericCommandResult(true, "Valores calculados, troca concluída com sucesso! ", new { });
        }
    }
}
