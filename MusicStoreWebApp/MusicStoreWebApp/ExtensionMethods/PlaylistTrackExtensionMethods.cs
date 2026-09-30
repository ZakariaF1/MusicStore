using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class PlaylistTrackExtensionMethods
    {
        public static PlaylistTrackViewModel ToViewModel(this PlaylistTrackResponse playlistTrackResponse)
        {
            return new PlaylistTrackViewModel
            {
                TrackId = playlistTrackResponse.Track.TrackId,
                TrackName = playlistTrackResponse.Track.Name
            };
        }
    }
}
