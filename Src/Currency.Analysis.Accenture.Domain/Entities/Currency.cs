
using Currency.Analysis.Accenture.Shared.Entities;

namespace Currency.Analysis.Accenture.Domain.Entities
{
    public class Currency: Entity
    {
        public decimal? Value { get; set; }
        public int? Applied { get; set; }
        public int? Replacement { get; set; }
        public string Date { get; set; }
        public decimal? OutputValue { get; set; }
        public string AppliedName { get; set; }
        public string ReplacementName { get; set; }

    }
}
