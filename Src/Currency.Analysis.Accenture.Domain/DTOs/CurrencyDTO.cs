using System;
using System.Collections.Generic;
using System.Text;

namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class CurrencyDTO
    {
        public string Name { get; set; }
        public string RateName { get; set; }
        public decimal RateValue { get; set; }
        public string Base { get; set; }
        public decimal Value { get; set; }
    }
}
