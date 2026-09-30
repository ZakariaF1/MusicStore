using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface ITrackApiMethods
    {
        Task<ApiResponse<IEnumerable<TrackResponse>>> ListTracksAsync();
        Task<ApiResponse<TrackResponse>> CreateTrackAsync(TrackCreateRequest request);
        Task<ApiResponse<TrackResponse>> GetTrackAsync(int trackId);
        Task<ApiResponse<TrackResponse>> DeleteTrackAsync(int trackId);
        Task<ApiResponse<TrackResponse>> UpdateTrackAsync(int trackId, TrackUpdateRequest request);
    }
}