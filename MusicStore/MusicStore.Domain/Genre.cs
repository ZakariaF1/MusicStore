
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
