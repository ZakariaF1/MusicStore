using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class GenreExtensionMethods
    {
        public static GenreUpdateRequest ToUpdateRequestDto(this GenreResponse genreResponse)
        {
            return new GenreUpdateRequest
            {
                Name = genreResponse.Name
            };
        }

        public static GenreViewModel ToViewModel(this GenreResponse genreResponse)
        {
            return new GenreViewModel
            {
                GenreId = genreResponse.GenreId,
                Name = genreResponse.Name
            };
        }
    }
}
