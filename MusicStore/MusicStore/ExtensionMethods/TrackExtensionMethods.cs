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
    public static class TrackeExtensionMethods
    {
        public static TrackResponse ToResponseDto(this Track track)
        {
            return new TrackResponse
            {
                TrackId = track.TrackId,
                Name = track.Name,
                Album = track.Album.ToResponseDto(),
                MediaType = track.MediaType.ToResponseDto(),
                Genre = track.Genre.ToResponseDto(),
                Composer = track.Composer,
                Milliseconds = track.Milliseconds,
                Bytes = track.Bytes,
                UnitPrice = track.UnitPrice
            };
        }

        public static TrackLiteResponse ToLiteResponseDto(this Track track)
        {
            return new TrackLiteResponse
            {
                TrackId = track.TrackId,
                Name = track.Name,
                AlbumId = track.AlbumId,
                MediaTypeId = track.MediaTypeId,
                GenreId = track.GenreId,
                Composer = track.Composer,
                Milliseconds = track.Milliseconds,
                Bytes = track.Bytes,
                UnitPrice = track.UnitPrice
            };
        }

        public static Track ToEntity(this TrackCreateRequest request)
        {
            return new Track
            {
                Name = request.Name,
                AlbumId = request.AlbumId,
                MediaTypeId = request.MediaTypeId,
                GenreId = request.GenreId,
                Composer = request.Composer,
                Milliseconds = request.Milliseconds,
                Bytes = request.Bytes,
                UnitPrice = request.UnitPrice
            };
        }

        public static Track UpdateTrack(this Track track, TrackUpdateRequest request)
        {
            track.Name = request.Name;
            track.AlbumId = request.AlbumId;
            track.MediaTypeId = request.MediaTypeId;
            track.GenreId = request.GenreId;
            track.Composer = request.Composer;
            track.Milliseconds = request.Milliseconds;
            track.Bytes = request.Bytes;
            track.UnitPrice = request.UnitPrice;

            return track;
        }
    }
}
