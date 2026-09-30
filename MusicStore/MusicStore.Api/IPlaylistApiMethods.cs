using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IPlaylistApiMethods
    {
        Task<ApiResponse<IEnumerable<PlaylistResponse>>> ListPlaylistsAsync();
        Task<ApiResponse<PlaylistResponse>> CreatePlaylistAsync(PlaylistCreateRequest request);
        Task<ApiResponse<PlaylistResponse>> GetPlaylistAsync(int playlistId);
        Task<ApiResponse<PlaylistResponse>> DeletePlaylistAsync(int playlistId);
        Task<ApiResponse<PlaylistResponse>> UpdatePlaylistAsync(int playlistId, PlaylistUpdateRequest request);
        Task<ApiResponse<IEnumerable<PlaylistTrackResponse>>> ListPlaylistTracksAsync(int playlistId);
        Task<ApiResponse<PlaylistTrackResponse>> CreatePlaylistTrackAsync(int playlistId, PlaylistTrackCreateRequest request);
        Task<ApiResponse<PlaylistTrackResponse>> DeletePlaylistTrackAsync(int playlistId, int playlistTrackId);
    }
}