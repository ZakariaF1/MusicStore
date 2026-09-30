
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Domain
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AlbumId { get; set; }
        [Required]
        [StringLength(160,MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
 
    }
}
