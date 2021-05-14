using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Infra.Data.DataContext;

namespace Currency.Analysis.Accenture.Application.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly CAASystemDataContext _context;

        public RecommendationController(CAASystemDataContext context)
        {
            _context = context;
        }

        // GET: Recommendation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currency.ToListAsync());
        }

        // GET: Recommendation/Details/5
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

        // GET: Recommendation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recommendation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,RateValue,Base,Value,Id")] CurrencyDTO currencyDTO)
        {
            if (ModelState.IsValid)
            {
                currencyDTO.Id = Guid.NewGuid();
                _context.Add(currencyDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currencyDTO);
        }

        // GET: Recommendation/Edit/5
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

        // POST: Recommendation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,RateValue,Base,Value,Id")] CurrencyDTO currencyDTO)
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
                return RedirectToAction(nameof(Index));
            }
            return View(currencyDTO);
        }

        // GET: Recommendation/Delete/5
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

        // POST: Recommendation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var currencyDTO = await _context.Currency.FindAsync(id);
            _context.Currency.Remove(currencyDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyDTOExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
