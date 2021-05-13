
namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class ExchangeRateDTO
    {
        public bool Success { get; set; }
        public int Timestamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public RateDTO Rates { get; set; }
    }
}
