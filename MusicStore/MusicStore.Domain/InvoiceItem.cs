
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class InvoiceItem
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        [Required]
        public int TrackId { get; set; }
        public Track Track { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1, 10)]
        public int Quantity { get; set; }
    }
}
