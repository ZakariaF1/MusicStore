using MusicStore.Api.Requests.CreateRequests;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class InvoiceFrontendUpdateRequest
    {
        public int InvoiceId { get; set; }
        public InvoiceUpdateRequest InvoiceUpdateRequest { get; set; }
        public IEnumerable<InvoiceItemCreateRequest> InvoiceItemCreateRequests { get; set; }
        public List<InvoiceItemUpdateRequest> InvoiceItemUpdateRequests { get; set; }
        public int[] InvoiceItemUpdateRequestIds { get; set; }
        public int[] InvoiceItemsForDeletion { get; set; }
        public int? CustomerId { get; set; }
        public bool IsFromCustomer { get; set; }
    }
}
