using MusicStore.Api.Requests.UpdateRequests;

namespace MusicStore.Api.Responses
{
    public class GenreFrontendUpdateResponse
    {
        public GenreUpdateRequest GenreUpdateRequest { get; set; }
        public GenreResponse GenreResponse { get; set; }
    }
}
