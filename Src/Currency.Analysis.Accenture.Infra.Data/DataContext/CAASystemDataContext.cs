using Currency.Analysis.Accenture.Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Analysis.Accenture.Infra.Data.DataContext
{
    public class CAASystemDataContext: DbContext
    {
        public CAASystemDataContext(DbContextOptions<CAASystemDataContext> options)
          : base(options) { }

        public DbSet<CurrencyDTO> Currency { get; set; }

        internal Task<List<CurrencyDTO>> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
