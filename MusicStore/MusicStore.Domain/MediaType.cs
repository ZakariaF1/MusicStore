
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class MediaType
    {
        public int MediaTypeId { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
