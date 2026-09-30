using System.Collections.Generic;

namespace MusicStore.Api.Requests.CreateRequests
{
    public class InvoiceFrontendCreateRequest
    {
        public InvoiceCreateRequest InvoiceCreateRequest { get; set; }
        public IEnumerable<InvoiceItemCreateRequest> InvoiceItemCreateRequests { get; set; }
        public int? CustomerId { get; set; }
        public bool IsFromCustomer { get; set; }
    }
}
