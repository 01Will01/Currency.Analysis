using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Currency.Analysis.Accenture.Domain.CommandHandlers;
using Currency.Analysis.Accenture.Domain.Commands;

namespace Currency.Analysis.Accenture.Application.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly CAASystemDataContext _context;

        public RecommendationController(CAASystemDataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> History()
        {
            return View(await _context.Currency.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyDTO = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ExchangeResgisterCommand command = new ExchangeResgisterCommand(data.Value, data.Applied, data.Replacement);

            await handler.Handle(command);
            if (ModelState.IsValid)
            {
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(History));
            }
            return View(data);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyDTO = await _context.Currency.FindAsync(id);
            if (currencyDTO == null)
            {
                return NotFound();
            }
            return View(currencyDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CurrencyDTO currencyDTO)
        {
            if (id != currencyDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currencyDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyDTOExists(currencyDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(History));
            }
            return View(currencyDTO);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currencyDTO = await _context.Currency
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currencyDTO == null)
            {
                return NotFound();
            }

            return View(currencyDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var currencyDTO = await _context.Currency.FindAsync(id);
            _context.Currency.Remove(currencyDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(History));
        }

        private bool CurrencyDTOExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
