using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public MediaTypeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<MediaType> MediaTypeBaseQuery()
        {
            IQueryable<MediaType> mediaTypesQuery = _unitOfWork.MediaTypes;
            return mediaTypesQuery;
        }

        public async Task<IEnumerable<MediaType>> ListMediaTypesAsync(IQueryable<MediaType> query, bool asNoTracking)
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

        public async Task CreateMediaTypeAsync(MediaType mediaType)
        {
            await _unitOfWork.MediaTypes.AddAsync(mediaType);
        }

        public async Task<MediaType> GetMediaTypeAsync(int mediaTypeId)
        {
            return await MediaTypeBaseQuery().Where(mediaType => mediaType.MediaTypeId == mediaTypeId).FirstOrDefaultAsync();
        }

        public void DeleteMediaType(MediaType mediaType)
        {
            _unitOfWork.Remove(mediaType);
        }

        public async Task<bool> MediaTypeExistsAsync(int mediaTypeId)
        {
            return await MediaTypeBaseQuery().AsNoTracking().AnyAsync(mediaType => mediaType.MediaTypeId == mediaTypeId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
