using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class InvoiceItemUpdateRequest
    {
        [Required]
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        [Range(1,10)]
        public int Quantity { get; set; }
    }
}
