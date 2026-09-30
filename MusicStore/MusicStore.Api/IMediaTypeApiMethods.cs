using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IMediaTypeApiMethods
    {
        Task<ApiResponse<IEnumerable<MediaTypeResponse>>> ListMediaTypesAsync();
        Task<ApiResponse<MediaTypeResponse>> CreateMediaTypeAsync(MediaTypeCreateRequest request);
        Task<ApiResponse<MediaTypeResponse>> GetMediaTypeAsync(int mediaTypeId);
        Task<ApiResponse<MediaTypeResponse>> DeleteMediaTypeAsync(int mediaTypeId);
        Task<ApiResponse<MediaTypeResponse>> UpdateMediaTypeAsync(int mediaTypeId, MediaTypeUpdateRequest request);
    }
}