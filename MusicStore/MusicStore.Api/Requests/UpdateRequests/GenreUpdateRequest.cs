using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class GenreUpdateRequest
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^[a-zA-Z ,.'&-]+$", ErrorMessage = "Please provide a proper name. Allowed characters are ( a-z, . , ' , &, - )")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Between 3 and 120 characters is required")]
        public string Name { get; set; }
    }
}
