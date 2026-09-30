using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.ExtensionMethods
{
    public static class ArtistExtensionMethods
    {
        public static ArtistResponse ToResponseDto(this Artist artist)
        {
            return new ArtistResponse
            {
                ArtistId = artist.ArtistId,
                Name = artist.Name
            };
        }

        public static Artist ToEntity(this ArtistCreateRequest request)
        {
            return new Artist
            {
                Name = request.Name
            };
        }

        public static Artist UpdateArtist(this Artist artist, ArtistUpdateRequest request)
        {

            artist.Name = request.Name;

            return artist;
        }
    }
}
