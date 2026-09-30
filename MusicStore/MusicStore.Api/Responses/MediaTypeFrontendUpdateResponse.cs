using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class MediaTypeFrontendUpdateResponse
    {
        public MediaTypeUpdateRequest MediaTypeUpdateRequest { get; set; }
        public MediaTypeResponse MediaTypeResponse { get; set; }
    }
}
