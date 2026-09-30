using System.ComponentModel.DataAnnotations;


namespace MusicStore.Api.Requests.CreateRequests
{
    public class TrackCreateRequest
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^(?=.*?[a-zA-Z])[0-9a-zA-Z /.'()&-]+$", ErrorMessage = "Please provide a proper track name. Allowed characters are ( a-z ,0-9, . , ' , (), &, - ) with at least one letter")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Between 3 and 200 characters is required")]
        public string Name { get; set; }

        public int? AlbumId { get; set; }

        [Required(ErrorMessage = "The media type Id field is required.")]
        public int MediaTypeId { get; set; }

        public int? GenreId { get; set; }

        [RegularExpression(@"^[a-zA-Z ,.'()-]+$", ErrorMessage = "Please provide a proper composer name. Allowed characters are ( a-z , . , ' , (), - )")]
        [StringLength(220, MinimumLength = 3, ErrorMessage = "Between 3 and 220 characters is required")]
        public string Composer { get; set; }

        [Required(ErrorMessage = "The milliseconds field is required.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please provide a proper number. Allowed characters are ( 0-9 )")]
        [Range(10000, 600000)]
        public int Milliseconds { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Please provide a proper number. Allowed characters are ( 0-9 )")]
        [Range(1000, 100000000)]
        public int? Bytes { get; set; }

        [Required(ErrorMessage = "The unit price field is required.")]
        public decimal UnitPrice { get; set; }
    }
}
