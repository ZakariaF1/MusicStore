using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class ArtistFrontendUpdateResponse
    {
        public ArtistUpdateRequest ArtistUpdateRequest { get; set; }
        public ArtistResponse ArtistResponse { get; set; }
    }
}
