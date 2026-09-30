
namespace MusicStore.Api.Responses
{
    public class InvoiceItemResponse
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public TrackResponse Track { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
