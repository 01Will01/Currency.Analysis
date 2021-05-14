using Currency.Analysis.Accenture.Domain.DTOs;
using Currency.Analysis.Accenture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Currency.Analysis.Accenture.Infra.Data.DataContext
{
    public class CAASystemDataContext : DbContext
    {
        public CAASystemDataContext(DbContextOptions<CAASystemDataContext> options)
          : base(options) { }

        public DbSet<Currencies> Currency { get; set; }
    }
}
