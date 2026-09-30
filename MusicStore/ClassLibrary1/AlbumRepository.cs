using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public AlbumRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Album> AlbumBaseQuery()
        {
            IQueryable<Album> albumsQuery = _unitOfWork.Albums;
            return albumsQuery;
        }

        public async Task<IEnumerable<Album>> ListAlbumsAsync(IQueryable<Album> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.Artist)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.Artist)
                    .ToListAsync();
            }
        }

        public async Task CreateAlbumAsync(Album album)
        {
            await _unitOfWork.Albums.AddAsync(album);
        }

        public async Task<Album> GetAlbumAsync(int albumId)
        {
            return await AlbumBaseQuery()
                .Where(album => album.AlbumId == albumId)
                .Include(p => p.Artist)
                .FirstOrDefaultAsync();
        }

        public void DeleteAlbum(Album album)
        {
            _unitOfWork.Remove(album);
        }

        public async Task<bool> AlbumExistsAsync(int? albumId)
        {
            return await AlbumBaseQuery()
                .AsNoTracking()
                .AnyAsync(album => album.AlbumId == albumId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
