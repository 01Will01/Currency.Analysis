
namespace Currency.Analysis.Accenture.Domain.DTOs.Relationships
{
    public class MessariDataDTO
    {
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Symbol { get; set; }
        public MessariMetricsDTO Metrics { get; set; }
    }
}
