using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IAlbumRepository
    {
        IQueryable<Album> AlbumBaseQuery();
        Task<IEnumerable<Album>> ListAlbumsAsync(IQueryable<Album> query, bool asNoTracking = false);
        Task CreateAlbumAsync(Album album);
        Task<Album> GetAlbumAsync(int albumId);
        void DeleteAlbum(Album album);
        Task<bool> AlbumExistsAsync(int? albumId);
        Task SaveAsync();
    }
}
