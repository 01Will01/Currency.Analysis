
namespace Currency.Analysis.Accenture.Domain.DTOs
{
    public class CoinloreParisDTO
    {
        public string Base { get; set; }
        public string Quote { get; set; }
        public decimal Volume { get; set; }
        public decimal Price { get; set; }
        public decimal PriceCurrency { get; set; }
        public decimal Time { get; set; }
    }
}
