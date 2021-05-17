using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Currency.Analysis.Accenture.Domain.CommandHandlers;
using Currency.Analysis.Accenture.Domain.Commands;
using Currency.Analysis.Accenture.Domain.Interfaces.Queries;
using Currency.Analysis.Accenture.Domain.Interfaces;
using Currency.Analysis.Accenture.Shared;
using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using System.Collections.Generic;
using ContosoUniversity.ViewModels;

namespace Currency.Analysis.Accenture.Application.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly CAASystemDataContext _context;
        private readonly IExchangeCurrencyQuery _currencyExchangeQuery;
        private readonly IExchangeCurrencyHttpQuery _exchangeRateQuery;

        public RecommendationController(CAASystemDataContext context, IExchangeCurrencyQuery currencyExchangeQuery, IExchangeCurrencyHttpQuery exchangeRateQuery)
        {
            _context = context;
            _currencyExchangeQuery = currencyExchangeQuery;
            _exchangeRateQuery = exchangeRateQuery;
        }

        public async Task<IActionResult> History()
        {
            return View(await _currencyExchangeQuery.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyDTO = await _currencyExchangeQuery.GetById((Guid)id);
            if (currencyDTO == null)
            {
                return NotFound();
            }

            return View(currencyDTO);
        }

        public async Task<IActionResult> Create()
        {
            (ViewBag.DataApplied, ViewBag.DataReplacement) = await _exchangeRateQuery.GetListCurrencyNames(Settings.COINLORE_CURRENCIES_URL, Settings.COINLORE_EXCHANGE_RATE_URL);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CurrencyDTO data, [FromServices] ExchangeRateCommandHandler handler)
        {

            if (ModelState.IsValid)
            {

                (var currenciesApplied, var currenciesReplacement) = await _exchangeRateQuery.GetListCurrencyNames(Settings.COINLORE_CURRENCIES_URL, Settings.COINLORE_EXCHANGE_RATE_URL);

                currenciesApplied.ForEach(currency => { if (data.Applied.Symbol == currency.Symbol) { data.Applied.Name = currency.Name; } });

                currenciesReplacement.ForEach(currency => { if (data.Replacement.Symbol == currency.Symbol) { data.Replacement.Name = currency.Name; }});

                ExchangeResgisterCommand command = new ExchangeResgisterCommand(data.Value, data.Applied, data.Replacement);
                await handler.Handle(command);

                return RedirectToAction(nameof(History));
            }

            (ViewBag.DataApplied, ViewBag.DataReplacement) = await _exchangeRateQuery.GetListCurrencyNames(Settings.COINLORE_CURRENCIES_URL, Settings.COINLORE_EXCHANGE_RATE_URL);

            return View(data);
        }

        private bool CurrencyDTOExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
