using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class PlaylistUpdateRequest
    {
        [Required(ErrorMessage = "The name field is required.")]
        [RegularExpression(@"^(?=.*?[a-zA-Z])[0-9a-zA-Z /.'()&-]+$", ErrorMessage = "Please provide a proper playlist name. Allowed characters are ( a-z ,0-9, . , ' , (), &, - ) with at least one letter")]
        [StringLength(120, MinimumLength = 3, ErrorMessage = "Between 3 and 120 characters is required")]
        public string Name { get; set; }
    }
}
