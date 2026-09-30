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
    public class TrackController : Controller
    {
        private readonly ITrackApiMethods _trackApiMethods;
        public TrackController(ITrackApiMethods trackApiMethods)
        {
            _trackApiMethods = trackApiMethods;
        }

        public async Task<IActionResult> GridListTracks([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<TrackResponse>> tracksApiResponse = await _trackApiMethods.ListTracksAsync();
            
            IEnumerable<TrackViewModel> tracksViewModel = tracksApiResponse.Result.Select(track => track.ToViewModel());

            DataSourceResult result = tracksViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> ListTracks()
        {
            ApiResponse<IEnumerable<TrackResponse>> tracksApiResponse = await _trackApiMethods.ListTracksAsync();

            IEnumerable<TrackViewModel> tracksViewModel = tracksApiResponse.Result.Select(track => track.ToViewModel());

            return Json(tracksViewModel);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<TrackResponse>> tracksApiResponse = await _trackApiMethods.ListTracksAsync();

            if (tracksApiResponse.HasException)
            {
                string errorMessage = tracksApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(TrackCreateRequest request)
        {
            ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.CreateTrackAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (trackApiResponse.HasException)
            {
                string errorMessage = trackApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int trackId)
        {
            ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.GetTrackAsync(trackId);

            TrackViewModel trackViewModel = trackApiResponse.Result.ToViewModel();

            return View(trackViewModel);
        }

        public async Task<ActionResult> GetTrack([FromBody] int trackId)
        {
            ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.GetTrackAsync(trackId);

            return Json(trackApiResponse.Result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] trackIds)
        {

            foreach (int trackId in trackIds)
            {
                ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.DeleteTrackAsync(trackId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (trackApiResponse.HasException)
                {
                    string errorMessage = trackApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Track") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int trackId)
        {
            ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.GetTrackAsync(trackId);

            TrackFrontendUpdateResponse trackFrontendResponse = new TrackFrontendUpdateResponse
            {
                TrackResponse = trackApiResponse.Result,
                TrackUpdateRequest = trackApiResponse.Result.ToUpdateRequestDto()
            };

            return View(trackFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int trackId, TrackFrontendUpdateResponse response)
        {
            TrackUpdateRequest updateRequest = response.TrackUpdateRequest;
            ApiResponse<TrackResponse> trackApiResponse = await _trackApiMethods.UpdateTrackAsync(trackId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (trackApiResponse.HasException)
            {
                string errorMessage = trackApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { trackId = trackApiResponse.Result.TrackId });
            }
        }
    }
}
