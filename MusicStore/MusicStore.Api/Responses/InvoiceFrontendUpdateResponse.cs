using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class InvoiceFrontendUpdateResponse
    {
        public InvoiceUpdateRequest InvoiceUpdateRequest { get; set; }
        public InvoiceResponse InvoiceResponse { get; set; }
        public CustomerResponse CustomerResponse { get; set; }
    }
}
