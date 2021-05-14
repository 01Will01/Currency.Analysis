using Currency.Analysis.Accenture.Domain.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Currency.Analysis.Accenture.Infra.Data.DataContext
{
    public class CAASystemDataContext : DbContext
    {
        public CAASystemDataContext(DbContextOptions<CAASystemDataContext> options)
          : base(options) { }

        public DbSet<CurrencyDTO> Currency { get; set; }
    }
}
