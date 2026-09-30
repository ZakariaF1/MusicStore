using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IMediaTypeRepository
    {
        IQueryable<MediaType> MediaTypeBaseQuery();
        Task<IEnumerable<MediaType>> ListMediaTypesAsync(IQueryable<MediaType> query, bool asNoTracking = false);
        Task CreateMediaTypeAsync(MediaType mediaType);
        Task<MediaType> GetMediaTypeAsync(int mediaTypeId);
        void DeleteMediaType(MediaType mediaType);
        Task<bool> MediaTypeExistsAsync(int mediaTypeId);
        Task SaveAsync();
    }
}
