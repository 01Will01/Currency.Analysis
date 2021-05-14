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

namespace Currency.Analysis.Accenture.Application.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly CAASystemDataContext _context;
        private readonly ICurrencyExchangeQuery _currencyExchangeQuery;

        public RecommendationController(CAASystemDataContext context, ICurrencyExchangeQuery currencyExchangeQuery)
        {
            _context = context;
            _currencyExchangeQuery = currencyExchangeQuery;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CurrencyDTO data, [FromServices] ExchangeRateCommandHandler handler)
        {
            
            if (ModelState.IsValid)
            {
                ExchangeResgisterCommand command = new ExchangeResgisterCommand(data.Value, data.Applied, data.Replacement);
                await handler.Handle(command);

                return RedirectToAction(nameof(History));
            }
            return View(data);
        }

        private bool CurrencyDTOExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
