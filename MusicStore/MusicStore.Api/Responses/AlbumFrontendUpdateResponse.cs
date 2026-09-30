using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class AlbumFrontendUpdateResponse
    {
        public AlbumUpdateRequest AlbumUpdateRequest { get; set; }
        public AlbumResponse AlbumResponse { get; set; }
    }
}
