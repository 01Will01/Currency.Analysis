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
        private readonly IExchangeCurrencyHttpQuery _exchangeRateQuery;
        private readonly IExchangeService _exchangeRateService;

        public ExchangeRateCommandHandler(IExchangeCurrencyHttpQuery exchangeRateQuery, IExchangeService exchangeRateService)
        {
            _exchangeRateQuery = exchangeRateQuery;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<GenericCommandResult> Handle(ExchangeResgisterCommand command)
        {
            command.Validate();

            if (command.Invalid) return new GenericCommandResult(false, "Valores de entrada inválidos", new { });

            CoinloreExchangeRateDTO coinlore = await _exchangeRateQuery.GetDataExchangeRate(Settings.COINLORE_EXCHANGE_RATE_URL);

            if (coinlore is null) { return new GenericCommandResult(false, "Taxa de cambio não localizada! ;(", new { }); }

            decimal Exchangerate = _exchangeRateService.GetExchangeRate(command.Replacement.Symbol, command.Applied.Symbol, coinlore);

            Currencies currencies =
                new Currencies(
                    value: command.Value,
                    appliedSymbol: command.Applied.Symbol,
                    replacementSymbol: command.Replacement.Symbol,
                    date: $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year} às {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second} hrs",
                    outputValue: Exchangerate * (decimal)command.Value,
                    appliedName: command.Applied.Name,
                    replacementName: command.Replacement.Name
                    );

            await _exchangeRateService.Register(currencies);

            return new GenericCommandResult(true, "Valores calculados, troca concluída com sucesso!", new { });
        }
    }
}
