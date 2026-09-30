using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class MediaTypeExtensionMethods
    {
        public static MediaTypeUpdateRequest ToUpdateRequestDto(this MediaTypeResponse mediaTypeResponse)
        {
            return new MediaTypeUpdateRequest
            {
                Name = mediaTypeResponse.Name
            };
        }

        public static MediaTypeViewModel ToViewModel(this MediaTypeResponse mediaTypeResponse)
        {
            return new MediaTypeViewModel
            {
                MediaTypeId = mediaTypeResponse.MediaTypeId,
                Name = mediaTypeResponse.Name
            };
        }
    }
}
