using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IArtistRepository
    {
        IQueryable<Artist> ArtistBaseQuery();
        Task<IEnumerable<Artist>> ListArtistsAsync(IQueryable<Artist> query, bool asNoTracking = false);
        Task CreateArtistAsync(Artist artist);
        Task<Artist> GetArtistAsync(int artistId);
        void DeleteArtist(Artist artist);
        Task<bool> ArtistExistsAsync(int artistId);
        Task SaveAsync();
    }
}
