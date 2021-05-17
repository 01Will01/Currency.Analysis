
using System.Collections.Generic;

namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class CoinloreExchangeRateDTO
    {
        public object Data { get; set; }
        public List<CoinloreParisDTO> Pairs { get; set; }
    }
}
