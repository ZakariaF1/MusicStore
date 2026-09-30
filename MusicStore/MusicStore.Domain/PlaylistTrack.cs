
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class PlaylistTrack
    {
        [Required]
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        [Required]
        public int TrackId { get; set; }
        public Track Track { get; set; }
    }
}
