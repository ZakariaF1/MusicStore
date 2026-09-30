using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IAlbumApiMethods
    {
        Task<ApiResponse<IEnumerable<AlbumResponse>>> ListAlbumsAsync();
        Task<ApiResponse<AlbumResponse>> CreateAlbumAsync(AlbumCreateRequest request);
        Task<ApiResponse<AlbumResponse>> GetAlbumAsync(int albumId);
        Task<ApiResponse<AlbumResponse>> DeleteAlbumAsync(int albumId);
        Task<ApiResponse<AlbumResponse>> UpdateAlbumAsync(int albumId, AlbumUpdateRequest request);
    }
}