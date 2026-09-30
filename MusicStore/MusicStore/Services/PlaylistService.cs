using MusicStore.Domain;
using MusicStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;

        }

        public async Task DeletePlaylistCascadeAsync(Playlist playlist)
        {
            IQueryable<PlaylistTrack> playlistTracksQuery = _playlistRepository.PlaylistTrackBaseQuery();

            playlistTracksQuery = playlistTracksQuery.Where(playlistTrack => playlistTrack.PlaylistId == playlist.PlaylistId);

            IEnumerable<PlaylistTrack> playlistTracks = await _playlistRepository.ListPlaylistTracksAsync(playlistTracksQuery);

            foreach (PlaylistTrack playlistTrack in playlistTracks)
            {
                _playlistRepository.DeletePlaylistTrack(playlistTrack);
            }
            _playlistRepository.DeletePlaylist(playlist);
        }
    }
}
