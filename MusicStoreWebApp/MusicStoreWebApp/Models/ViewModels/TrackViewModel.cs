namespace MusicStoreWebApp.Models.ViewModels
{
    public class TrackViewModel
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        public string GenreId { get; set; }
        public string GenreName { get; set; }
        public string Composer { get; set; }
        public string Milliseconds { get; set; }
        public string Bytes { get; set; }
        public string UnitPrice { get; set; }
    }
}
