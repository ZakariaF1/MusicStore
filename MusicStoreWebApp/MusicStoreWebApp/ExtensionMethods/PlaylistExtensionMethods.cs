using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class PlaylistExtensionMethods
    {
        public static PlaylistUpdateRequest ToUpdateRequestDto(this PlaylistResponse playlistResponse)
        {
            return new PlaylistUpdateRequest
            {
                Name = playlistResponse.Name
            };
        }

        public static PlaylistViewModel ToViewModel(this PlaylistResponse playlistResponse)
        {
            return new PlaylistViewModel
            {
                PlaylistId = playlistResponse.PlaylistId,
                Name = playlistResponse.Name
            };
        }
    }
}
