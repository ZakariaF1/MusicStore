using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public PlaylistRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Playlist> PlaylistBaseQuery()
        {
            IQueryable<Playlist> playlistsQuery = _unitOfWork.Playlists;
            return playlistsQuery;
        }

        public IQueryable<PlaylistTrack> PlaylistTrackBaseQuery()
        {
            IQueryable<PlaylistTrack> playlistTracksQuery = _unitOfWork.PlaylistTrack;
            return playlistTracksQuery;
        }

        public IQueryable<Track> TrackBaseQuery()
        {
            IQueryable<Track> tracksQuery = _unitOfWork.Tracks;
            return tracksQuery;
        }

        public async Task<IEnumerable<Playlist>> ListPlaylistsAsync(IQueryable<Playlist> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query.AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task CreatePlaylistAsync(Playlist playlist)
        {
            await _unitOfWork.Playlists.AddAsync(playlist);
        }

        public async Task<Playlist> GetPlaylistAsync(int playlistId)
        {
            return await PlaylistBaseQuery().Where(playlist => playlist.PlaylistId == playlistId).FirstOrDefaultAsync();
        }

        public void DeletePlaylist(Playlist playlist)
        {
            _unitOfWork.Remove(playlist);
        }

        public async Task<IEnumerable<PlaylistTrack>> ListPlaylistTracksAsync(IQueryable<PlaylistTrack> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.Playlist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Album)
                            .ThenInclude(p => p.Artist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Genre)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.MediaType)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.Playlist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Album)
                            .ThenInclude(p => p.Artist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Genre)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.MediaType)
                    .ToListAsync();
            }
        }

        public async Task CreatePlaylistTrackAsync(PlaylistTrack playlistTrack)
        {
            await _unitOfWork.PlaylistTrack.AddAsync(playlistTrack);
        }

        public async Task<PlaylistTrack> GetPlaylistTrackAsync(int playlistId, int trackId)
        {
            return await PlaylistTrackBaseQuery()
                .Where(playlistTrack => playlistTrack.PlaylistId == playlistId && playlistTrack.TrackId == trackId)
                .Include(p => p.Playlist)
                .Include(p => p.Track)
                    .ThenInclude(p => p.Album)
                        .ThenInclude(p => p.Artist)
                .Include(p => p.Track)
                    .ThenInclude(p => p.Genre)
                .Include(p => p.Track)
                    .ThenInclude(p => p.MediaType)
                .FirstOrDefaultAsync();
        }


        public void DeletePlaylistTrack(PlaylistTrack playlistTrack)
        {
            _unitOfWork.Remove(playlistTrack);
        }

        public async Task<bool> PlaylistExistsAsync(int playlistId)
        {
            return await PlaylistBaseQuery().AsNoTracking().AnyAsync(playlist => playlist.PlaylistId == playlistId);
        }

        public async Task<bool> TrackExistsAsync(int trackId)
        {

            return await TrackBaseQuery().AsNoTracking().AnyAsync(track => track.TrackId == trackId);
        }

        public async Task<bool> PlaylistTrackExistsAsync(int playlistId, int trackId)
        {
            IQueryable<PlaylistTrack> playlistTracksQuery = PlaylistTrackBaseQuery();

            playlistTracksQuery = playlistTracksQuery.Where(playlistTrack => playlistTrack.PlaylistId == playlistId);

            return await playlistTracksQuery.AnyAsync(track => track.TrackId == trackId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
