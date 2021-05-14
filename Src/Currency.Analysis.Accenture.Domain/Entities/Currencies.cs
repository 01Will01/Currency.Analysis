
using Currency.Analysis.Accenture.Shared.Entities;

namespace Currency.Analysis.Accenture.Domain.Entities
{
    public class Currencies : Entity
    {
        public Currencies(decimal? value, int? applied, int? replacement, string date, decimal? outputValue, string appliedName, string replacementName)
        {
            Value = value;
            Applied = applied;
            Replacement = replacement;
            Date = date;
            OutputValue = outputValue;
            AppliedName = appliedName;
            ReplacementName = replacementName;
        }

        public decimal? Value { get; set; }
        public int? Applied { get; set; }
        public int? Replacement { get; set; }
        public string Date { get; set; }
        public decimal? OutputValue { get; set; }
        public string AppliedName { get; set; }
        public string ReplacementName { get; set; }

    }
}
