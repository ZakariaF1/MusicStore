using System.ComponentModel.DataAnnotations;


namespace MusicStore.Api.Requests.CreateRequests
{
    public class ArtistCreateRequest
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^(?=.*?[a-zA-Z])[a-zA-Z0-9 ,.'-]+$", ErrorMessage = "Please provide a proper name. Allowed characters are ( a-z , . , ' , - ) with at least one letter")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Between 3 and 120 characters is required")]
        public string Name { get; set; }
    }
}
