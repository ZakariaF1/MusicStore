using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MusicStore.Api.Requests.UpdateRequests
{
    public class PlaylistTrackUpdateRequest
    {
        [Required]
        public int TrackId { get; set; }
    }
}
