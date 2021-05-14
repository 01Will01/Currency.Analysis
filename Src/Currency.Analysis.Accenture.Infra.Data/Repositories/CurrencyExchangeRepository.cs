
using Currency.Analysis.Accenture.Domain.Entities;
using Currency.Analysis.Accenture.Domain.Interfaces.Repositories;
using Currency.Analysis.Accenture.Infra.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Infra.Data.Repositories
{
    public class CurrencyExchangeRepository : ICurrencyExchangeRepository
    {
        private readonly CAASystemDataContext _context;

        public CurrencyExchangeRepository(CAASystemDataContext context)
        {
            _context = context;
        }

        public async Task Register(Currencies data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
        }
    }
}
