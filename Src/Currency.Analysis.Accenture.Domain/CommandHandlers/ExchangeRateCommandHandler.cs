using Currency.Analysis.Accenture.Domain.Commands;
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Domain.Interfaces.Services;
using Currency.Analysis.Accenture.Shared;
using Currency.Analysis.Accenture.Shared.Commands;
using Currency.Analysis.Accenture.Shared.Handlers.Interfaces;
using System;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Domain.CommandHandlers
{

    public class ExchangeRateCommandHandler : IHandler<ExchangeResgisterCommand>
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

            string type = _exchangeRateService.GetType(command.Replacement);

            MessariExchangeRateDTO messari = _exchangeRateQuery.GetOfficial(Settings.URLIntegrationOfficial, type);

            if (messari is null) { return new GenericCommandResult(false, "Taxa de cambio não localizada! ;(", new { }); }

            decimal Exchangerate = _exchangeRateService.GetExchangeRate(command.Replacement, command.Applied, messari);
            Currencies currencies =
                new Currencies(
                    value: command.Value,
                    applied: command.Applied,
                    replacement: command.Replacement,
                    date: $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year} às {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} hrs",
                    outputValue: Exchangerate * (decimal)command.Value,
                    appliedName: _exchangeRateService.GetCurrencyName(command.Applied),
                    replacementName: _exchangeRateService.GetCurrencyName(command.Replacement)
                    );

            await _exchangeRateService.Register(currencies);

            return new GenericCommandResult(true, "Valores calculados, troca concluída com sucesso!", new { });
        }
    }
}
