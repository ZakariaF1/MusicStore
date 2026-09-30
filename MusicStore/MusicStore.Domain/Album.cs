
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required]
        [StringLength(160,MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
 
    }
}
