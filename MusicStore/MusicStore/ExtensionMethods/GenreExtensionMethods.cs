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
    public static class GenreExtensionMethods
    {
        public static GenreResponse ToResponseDto(this Genre genre)
        {
            if (genre == null)
            {
                return null;
            } else
            {
                return new GenreResponse
                {
                    GenreId = genre.GenreId,
                    Name = genre.Name
                };
            }
        }

        public static Genre ToEntity(this GenreCreateRequest request)
        {
            return new Genre
            {
                Name = request.Name
            };
        }

        public static Genre UpdateGenre(this Genre genre, GenreUpdateRequest request)
        {

            genre.Name = request.Name;

            return genre;
        }
    }
}
