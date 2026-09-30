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
    public static class PlaylistTrackExtensionMethods
    {
        public static PlaylistTrackResponse ToResponseDto(this PlaylistTrack playlistTrack)
        {
            return new PlaylistTrackResponse
            {
                Playlist = playlistTrack.Playlist.ToResponseDto(),
                Track = playlistTrack.Track.ToResponseDto()
            };
        }

        public static PlaylistTrackLiteResponse ToLiteResponseDto(this PlaylistTrack playlistTrack)
        {
            return new PlaylistTrackLiteResponse
            {
                PlaylistId = playlistTrack.PlaylistId,
                TrackId = playlistTrack.TrackId
            };
        }

        public static PlaylistTrack ToEntity(this PlaylistTrackCreateRequest request)
        {
            return new PlaylistTrack
            {
                TrackId = request.TrackId
            };
        }

        //public static PlaylistTrack UpdatePlaylistTrack(this PlaylistTrack playlistTrack, PlaylistTrackRequest request)
        //{

        //    playlistTrack.TrackId = request.TrackId;

        //    return playlistTrack;
        //}
    }
}
