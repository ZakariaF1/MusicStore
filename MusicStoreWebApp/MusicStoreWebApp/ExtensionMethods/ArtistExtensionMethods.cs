using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class ArtistExtensionMethods
    {
        public static ArtistUpdateRequest ToUpdateRequestDto(this ArtistResponse artistResponse)
        {
            return new ArtistUpdateRequest
            {
                Name = artistResponse.Name
            };
        }

        public static ArtistViewModel ToViewModel(this ArtistResponse artistResponse)
        {
            return new ArtistViewModel
            {
                ArtistId = artistResponse.ArtistId,
                Name = artistResponse.Name
            };
        }
    }
}
