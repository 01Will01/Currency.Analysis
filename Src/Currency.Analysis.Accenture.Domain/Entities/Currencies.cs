
using Currency.Analysis.Accenture.Shared.Entities;

namespace Currency.Analysis.Accenture.Domain.Entities
{
    public class Currencies : Entity
    {
        public Currencies(decimal? value, string appliedSymbol, string replacementSymbol, string date, decimal? outputValue, string appliedName, string replacementName)
        {
            Value = value;
            AppliedSymbol = appliedSymbol;
            ReplacementSymbol = replacementSymbol;
            Date = date;
            OutputValue = outputValue;
            AppliedName = appliedName;
            ReplacementName = replacementName;
        }

        public decimal? Value { get; set; }
        public string AppliedSymbol { get; set; }
        public string ReplacementSymbol { get; set; }
        public string Date { get; set; }
        public decimal? OutputValue { get; set; }
        public string AppliedName { get; set; }
        public string ReplacementName { get; set; }

    }
}
