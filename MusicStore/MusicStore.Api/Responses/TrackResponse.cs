namespace MusicStore.Api.Responses
{
    public class TrackResponse
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public AlbumResponse Album { get; set; }
        public MediaTypeResponse MediaType { get; set; }
        public GenreResponse Genre { get; set; }
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
