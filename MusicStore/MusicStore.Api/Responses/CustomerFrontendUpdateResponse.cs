using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class CustomerFrontendUpdateResponse
    {
        public CustomerUpdateRequest CustomerUpdateRequest { get; set; }
        public CustomerResponse CustomerResponse { get; set; }
    }
}
