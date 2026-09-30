using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;

namespace MusicStore.ExtensionMethods
{
    public static class AlbumExtensionMethods
    {
        public static AlbumResponse ToResponseDto(this Album album)
        {
            if (album == null)
            {
                return null;
            }
            else
            {
                return new AlbumResponse
                {
                    AlbumId = album.AlbumId,
                    Title = album.Title,
                    Artist = album.Artist.ToResponseDto()
                };
            }
        }

        public static AlbumLiteResponse ToLiteResponseDto(this Album album)
        {
            return new AlbumLiteResponse
            {
                AlbumId = album.AlbumId,
                Title = album.Title,
                ArtistId = album.ArtistId
            };
        }

        public static Album ToEntity(this AlbumCreateRequest request)
        {
            return new Album
            {
                Title = request.Title,
                ArtistId = request.ArtistId
            };
        }

        public static Album UpdateAlbum(this Album album, AlbumUpdateRequest request)
        {

            album.Title = request.Title;
            album.ArtistId = request.ArtistId;

            return album;
        }
    }
}
