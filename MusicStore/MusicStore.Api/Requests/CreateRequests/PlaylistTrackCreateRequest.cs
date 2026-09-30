using System.ComponentModel.DataAnnotations;

namespace MusicStore.Api.Requests.CreateRequests
{
    public class PlaylistTrackCreateRequest
    {
        [Required]
        public int TrackId { get; set; }
    }
}
