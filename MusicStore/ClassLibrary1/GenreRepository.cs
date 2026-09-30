using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class GenreRepository : IGenreRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public GenreRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Genre> GenreBaseQuery()
        {
            IQueryable<Genre> genresQuery = _unitOfWork.Genres;
            return genresQuery;
        }

        public async Task<IEnumerable<Genre>> ListGenresAsync(IQueryable<Genre> query, bool asNoTracking)
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

        public async Task CreateGenreAsync(Genre genre)
        {
           await _unitOfWork.Genres.AddAsync(genre);
        }

        public async Task<Genre> GetGenreAsync(int genreId)
        {
            return await GenreBaseQuery().Where(genre => genre.GenreId == genreId).FirstOrDefaultAsync();
        }

        public void DeleteGenre(Genre genre)
        {
            _unitOfWork.Remove(genre);
        }

        public async Task<bool> GenreExistsAsync(int? genreId)
        {
            return await GenreBaseQuery().AsNoTracking().AnyAsync(genre => genre.GenreId == genreId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
