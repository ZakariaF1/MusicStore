using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public ArtistRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Artist> ArtistBaseQuery()
        {
            IQueryable<Artist> artistsQuery = _unitOfWork.Artists;
            return artistsQuery;
        }

        public async Task<IEnumerable<Artist>> ListArtistsAsync(IQueryable<Artist> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query.AsNoTracking()
                    .AsNoTracking()
                    .ToListAsync();
            }
            else
            {
                return await query
                    .ToListAsync();
            }
        }

        public async Task CreateArtistAsync(Artist artist)
        {
            await _unitOfWork.Artists.AddAsync(artist);
        }

        public async Task<Artist> GetArtistAsync(int artistId)
        {
            return await ArtistBaseQuery()
                .Where(artist => artist.ArtistId == artistId)
                .FirstOrDefaultAsync();
        }

        public void DeleteArtist(Artist artist)
        {
            _unitOfWork.Remove(artist);
        }

        public async Task<bool> ArtistExistsAsync(int artistId)
        {
            return await ArtistBaseQuery()
                .AsNoTracking()
                .AnyAsync(artist => artist.ArtistId == artistId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
