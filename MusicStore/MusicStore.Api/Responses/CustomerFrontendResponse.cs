using System.Collections.Generic;

namespace MusicStore.Api.Responses
{
    public class CustomerFrontendResponse
    {
        public CustomerResponse CustomerResponse { get; set; }
        public IEnumerable<InvoiceResponse> InvoiceResponse { get; set; }
        public IEnumerable<TrackResponse> TrackResponse { get; set; }
    }
}
