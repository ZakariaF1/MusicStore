using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class TrackFrontendUpdateResponse
    {
        public TrackUpdateRequest TrackUpdateRequest { get; set; }
        public TrackResponse TrackResponse { get; set; }
    }
}
