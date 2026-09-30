
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Domain
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        [StringLength(120, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
