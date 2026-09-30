using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class TrackExtensionMethods
    {
        public static TrackUpdateRequest ToUpdateRequestDto(this TrackResponse trackResponse)
        {
            return new TrackUpdateRequest
            {
                Name = trackResponse.Name,
                AlbumId = trackResponse.Album?.AlbumId,
                MediaTypeId = trackResponse.MediaType.MediaTypeId,
                GenreId = trackResponse.Genre?.GenreId,
                Composer = trackResponse.Composer,
                Milliseconds = trackResponse.Milliseconds,
                Bytes = trackResponse.Bytes,
                UnitPrice = trackResponse.UnitPrice
            };
        }

        public static TrackViewModel ToViewModel(this TrackResponse trackResponse)
        {
            return new TrackViewModel
            {
                TrackId = trackResponse.TrackId,
                Name = trackResponse.Name,
                AlbumId = trackResponse.Album?.AlbumId.ToString(),
                AlbumTitle = trackResponse.Album?.Title,
                MediaTypeId = trackResponse.MediaType.MediaTypeId.ToString(),
                MediaTypeName = trackResponse.MediaType.Name,
                GenreId = trackResponse.Genre?.GenreId.ToString(),
                GenreName = trackResponse.Genre?.Name,
                Composer = trackResponse.Composer,
                Milliseconds = trackResponse.Milliseconds.ToString(),
                Bytes = trackResponse.Bytes.ToString(),
                UnitPrice = trackResponse.UnitPrice.ToString()
            };
        }
    }
}
