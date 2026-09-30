using System.ComponentModel.DataAnnotations;


namespace MusicStore.Api.Requests.CreateRequests
{
    public class GenreCreateRequest
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^[a-zA-Z ,.'&-]+$", ErrorMessage = "Please provide a proper name. Allowed characters are ( a-z, . , ' , &, - )")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Between 3 and 120 characters is required")]
        public string Name { get; set; }
    }
}
