
using Currency.Analysis.Accenture.Domain.DTOs.Relationships;
using System.Collections.Generic;

namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class MessariExchangeRateDTO
    {
        public MessariStatusDTO Status { get; set; }
        public List<MessariDataDTO> Data {get; set;}
    }
}
