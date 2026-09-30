namespace MusicStore.Api.Responses
{
    public class AlbumResponse
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public ArtistResponse Artist { get; set; }
    }
}
