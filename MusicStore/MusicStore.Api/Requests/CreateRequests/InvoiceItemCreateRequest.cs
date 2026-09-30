using System.ComponentModel.DataAnnotations;


namespace MusicStore.Api.Requests.CreateRequests
{
    public class InvoiceItemCreateRequest
    {
        [Required]
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }
    }
}
