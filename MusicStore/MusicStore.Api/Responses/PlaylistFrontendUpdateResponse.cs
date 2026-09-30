using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class PlaylistFrontendUpdateResponse
    {
        public PlaylistUpdateRequest PlaylistUpdateRequest { get; set; }
        public PlaylistResponse PlaylistResponse { get; set; }
    }
}
