using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class TrackRepository : ITrackRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public TrackRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Track> TrackBaseQuery()
        {
            IQueryable<Track> tracksQuery = _unitOfWork.Tracks;
            return tracksQuery;
        }

        public async Task<IEnumerable<Track>> ListTracksAsync(IQueryable<Track> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.Album)
                        .ThenInclude(p => p.Artist)
                    .Include(p => p.MediaType)
                    .Include(p => p.Genre)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.Album)
                        .ThenInclude(p => p.Artist)
                    .Include(p => p.MediaType)
                    .Include(p => p.Genre)
                    .ToListAsync();
            }
        }

        public async Task CreateTrackAsync(Track track)
        {
            await _unitOfWork.Tracks.AddAsync(track);
        }

        public async Task<Track> GetTrackAsync(int trackId)
        {
            return await TrackBaseQuery()
                .Where(track => track.TrackId == trackId)
                .Include(p => p.Album)
                    .ThenInclude(p => p.Artist)
                .Include(p => p.MediaType)
                .Include(p => p.Genre).FirstOrDefaultAsync();
        }

        public void DeleteTrack(Track track)
        {
            _unitOfWork.Remove(track);
        }

        public async Task<bool> TrackExistsAsync(int trackId)
        {
            return await TrackBaseQuery().AsNoTracking().AnyAsync(track => track.TrackId == trackId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
