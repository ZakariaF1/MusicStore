using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IArtistApiMethods
    {
        Task<ApiResponse<IEnumerable<ArtistResponse>>> ListArtistsAsync();
        Task<ApiResponse<ArtistResponse>> CreateArtistAsync(ArtistCreateRequest request);
        Task<ApiResponse<ArtistResponse>> GetArtistAsync(int artistId);
        Task<ApiResponse<ArtistResponse>> DeleteArtistAsync(int artistId);
        Task<ApiResponse<ArtistResponse>> UpdateArtistAsync(int artistId, ArtistUpdateRequest request);
    }
}