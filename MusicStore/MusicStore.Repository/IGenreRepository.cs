using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IGenreRepository
    {
        IQueryable<Genre> GenreBaseQuery();
        Task<IEnumerable<Genre>> ListGenresAsync(IQueryable<Genre> query, bool asNoTracking = false);
        Task CreateGenreAsync(Genre genre);
        Task<Genre> GetGenreAsync(int genreId);
        void DeleteGenre(Genre genre);
        Task<bool> GenreExistsAsync(int? genreId);
        Task SaveAsync();
    }
}
