
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Domain
{
    public class MediaType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediaTypeId { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
