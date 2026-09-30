using System.ComponentModel.DataAnnotations;

namespace MusicStore.Api.Requests.CreateRequests
{
    public class AlbumCreateRequest
    {
        [Required(ErrorMessage = "The title field is required.")]
        [RegularExpression(@"^(?=.*?[a-zA-Z])[0-9a-zA-Z /.'()&-]+$", ErrorMessage = "Please provide a proper album title. Allowed characters are ( a-z ,0-9, . , ' , (), &, - ) with at least one letter")]
        [StringLength(160, MinimumLength = 3, ErrorMessage = "Between 3 and 160 characters is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The artist Id field is required.")]
        public int ArtistId { get; set; }
    }
}
