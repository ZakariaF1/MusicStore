using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Api;
using MusicStore.Api.Responses;
using MusicStore.Api.Requests.CreateRequests;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStoreWebApp.ExtensionMethods;
using MusicStoreWebApp.Models.ViewModels;
using System.Linq;

namespace MusicStoreWebApp.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistApiMethods _playlistApiMethods;
        public PlaylistController(IPlaylistApiMethods playlistApiMethods)
        {
            _playlistApiMethods = playlistApiMethods;
        }

        public async Task<IActionResult> GridListPlaylists([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<PlaylistResponse>> playlistsApiResponse = await _playlistApiMethods.ListPlaylistsAsync();

            IEnumerable<PlaylistViewModel> playlistsViewModel = playlistsApiResponse.Result.Select(playlist => playlist.ToViewModel());

            DataSourceResult result = playlistsViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> GridListPlaylistTracks([DataSourceRequest] DataSourceRequest request, int playlistId)
        {
            ApiResponse<IEnumerable<PlaylistTrackResponse>> playlistTracksApiResponse = await _playlistApiMethods.ListPlaylistTracksAsync(playlistId);

            IEnumerable<PlaylistTrackViewModel> playlistsTracksViewModel = playlistTracksApiResponse.Result.Select(playlistTrack => playlistTrack.ToViewModel());

            DataSourceResult result = playlistsTracksViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<PlaylistResponse>> playlistsApiResponse = await _playlistApiMethods.ListPlaylistsAsync();

            if (playlistsApiResponse.HasException)
            {
                string errorMessage = playlistsApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlaylistCreateRequest request)
        {
            ApiResponse<PlaylistResponse> playlistApiResponse = await _playlistApiMethods.CreatePlaylistAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (playlistApiResponse.HasException)
            {
                string errorMessage = playlistApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int playlistId)
        {
            ApiResponse<PlaylistResponse> playlistApiResponse = await _playlistApiMethods.GetPlaylistAsync(playlistId);

            PlaylistViewModel playlistViewModel = playlistApiResponse.Result.ToViewModel();

            return View(playlistViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] playlistIds)
        {

            foreach (int playlistId in playlistIds)
            {
                ApiResponse<PlaylistResponse> playlistApiResponse = await _playlistApiMethods.DeletePlaylistAsync(playlistId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (playlistApiResponse.HasException)
                {
                    string errorMessage = playlistApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Playlist") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int playlistId)
        {
            ApiResponse<PlaylistResponse> playlistApiResponse = await _playlistApiMethods.GetPlaylistAsync(playlistId);

            PlaylistFrontendUpdateResponse playlistFrontendResponse = new PlaylistFrontendUpdateResponse
            {
                PlaylistResponse = playlistApiResponse.Result,
                PlaylistUpdateRequest = playlistApiResponse.Result.ToUpdateRequestDto()
            };

            return View(playlistFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int playlistId, PlaylistFrontendUpdateResponse response)
        {
            PlaylistUpdateRequest updateRequest = response.PlaylistUpdateRequest;
            ApiResponse<PlaylistResponse> playlistApiResponse = await _playlistApiMethods.UpdatePlaylistAsync(playlistId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (playlistApiResponse.HasException)
            {
                string errorMessage = playlistApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { playlistId = playlistApiResponse.Result.PlaylistId });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlaylistTrack([FromBody] int[] data)
        {
            PlaylistTrackCreateRequest request = new PlaylistTrackCreateRequest
            {
                TrackId = data[1]
            };

            ApiResponse<PlaylistTrackResponse> playlistTrackApiResponse = await _playlistApiMethods.CreatePlaylistTrackAsync(data[0], request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (playlistTrackApiResponse.HasException)
            {
                string errorMessage = playlistTrackApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return Json(new { redirectToUrl = Url.Action("Details", "Playlist", new { playlistId = data[0] }) });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePlaylistTracks([FromBody] List<int[]> deleteRequest)
        {
            foreach (int playlistTrackId in deleteRequest[1])
            {
                ApiResponse<PlaylistTrackResponse> playlistTrackApiResponse = await _playlistApiMethods.DeletePlaylistTrackAsync(deleteRequest[0][0], playlistTrackId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (playlistTrackApiResponse.HasException)
                {
                    string errorMessage = playlistTrackApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Details", "Playlist", new { playlistId = deleteRequest[0][0] }) });
        }
    }
}
