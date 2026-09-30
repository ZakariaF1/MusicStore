using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface ITrackRepository
    {
        IQueryable<Track> TrackBaseQuery();
        Task<IEnumerable<Track>> ListTracksAsync(IQueryable<Track> query, bool asNoTracking = false);
        Task CreateTrackAsync(Track track);
        Task<Track> GetTrackAsync(int trackId);
        void DeleteTrack(Track track);
        Task<bool> TrackExistsAsync(int trackId);
        Task SaveAsync();
    }
}
