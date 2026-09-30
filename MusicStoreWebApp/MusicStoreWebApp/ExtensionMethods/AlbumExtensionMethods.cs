using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class AlbumExtensionMethods
    {
        public static AlbumUpdateRequest ToUpdateRequestDto(this AlbumResponse albumResponse)
        {
            return new AlbumUpdateRequest
            {
                Title = albumResponse.Title,
                ArtistId = albumResponse.Artist.ArtistId
            };
        }

        public static AlbumViewModel ToViewModel(this AlbumResponse albumResponse)
        {
            return new AlbumViewModel
            {
                AlbumId = albumResponse.AlbumId,
                Title = albumResponse.Title,
                ArtistId = albumResponse.Artist.ArtistId.ToString(),
                ArtistName = albumResponse.Artist.Name
            };
        }
    }
}
