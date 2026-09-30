
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class Track
    {
        public int TrackId { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public int? AlbumId { get; set; }
        public Album Album { get; set; }
        public int MediaTypeId { get; set; }
        public MediaType MediaType { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        [MaxLength(220)]
        public string Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
