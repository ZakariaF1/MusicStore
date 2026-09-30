using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IPlaylistRepository
    {
        IQueryable<Playlist> PlaylistBaseQuery();
        IQueryable<PlaylistTrack> PlaylistTrackBaseQuery();
        IQueryable<Track> TrackBaseQuery();
        Task<IEnumerable<Playlist>> ListPlaylistsAsync(IQueryable<Playlist> query, bool asNoTracking = false);
        Task CreatePlaylistAsync(Playlist playlist);
        Task<Playlist> GetPlaylistAsync(int playlistId);
        void DeletePlaylist(Playlist playlist);
        Task<IEnumerable<PlaylistTrack>> ListPlaylistTracksAsync(IQueryable<PlaylistTrack> query, bool asNoTracking = false);
        Task CreatePlaylistTrackAsync(PlaylistTrack playlistTrack);
        Task<PlaylistTrack> GetPlaylistTrackAsync(int playlistId, int trackId);
        void DeletePlaylistTrack(PlaylistTrack playlistTrack);
        Task<bool> PlaylistExistsAsync(int playlistId);
        Task<bool> TrackExistsAsync(int trackId);
        Task<bool> PlaylistTrackExistsAsync(int playlistId, int trackId);
        Task SaveAsync();
    }
}
