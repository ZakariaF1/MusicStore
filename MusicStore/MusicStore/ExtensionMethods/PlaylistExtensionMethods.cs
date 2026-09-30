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
    public static class PlaylistExtensionMethods
    {
        public static PlaylistResponse ToResponseDto(this Playlist playlist)
        {
            return new PlaylistResponse
            {
                PlaylistId = playlist.PlaylistId,
                Name = playlist.Name
            };
        }

        public static Playlist ToEntity(this PlaylistCreateRequest request)
        {
            return new Playlist
            {
                Name = request.Name
            };
        }

        public static Playlist UpdatePlaylist(this Playlist playlist, PlaylistUpdateRequest request)
        {

            playlist.Name = request.Name;

            return playlist;
        }
    }
}
