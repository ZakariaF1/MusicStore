using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IGenreApiMethods
    {
        Task<ApiResponse<IEnumerable<GenreResponse>>> ListGenresAsync();
        Task<ApiResponse<GenreResponse>> CreateGenreAsync(GenreCreateRequest request);
        Task<ApiResponse<GenreResponse>> GetGenreAsync(int genreId);
        Task<ApiResponse<GenreResponse>> DeleteGenreAsync(int genreId);
        Task<ApiResponse<GenreResponse>> UpdateGenreAsync(int genreId, GenreUpdateRequest request);
    }
}