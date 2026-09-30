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
    public class ArtistController : Controller
    {
        private readonly IArtistApiMethods _artistApiMethods;
        public ArtistController(IArtistApiMethods artistApiMethods)
        {
            _artistApiMethods = artistApiMethods;
        }

        public async Task<IActionResult> GridListArtists([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<ArtistResponse>> artistsApiResponse = await _artistApiMethods.ListArtistsAsync();

            IEnumerable<ArtistViewModel> artistsViewModel = artistsApiResponse.Result.Select(artist => artist.ToViewModel());

            DataSourceResult result = artistsViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> ListArtists()
        {
            ApiResponse<IEnumerable<ArtistResponse>> artistsApiResponse = await _artistApiMethods.ListArtistsAsync();

            return Json(artistsApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<ArtistResponse>> artistsApiResponse = await _artistApiMethods.ListArtistsAsync();

            if (artistsApiResponse.HasException)
            {
                string errorMessage = artistsApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(ArtistCreateRequest request)
        {
            ApiResponse<ArtistResponse > artistApiResponse = await _artistApiMethods.CreateArtistAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (artistApiResponse.HasException)
            {
                string errorMessage = artistApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int artistId)
        {
            ApiResponse<ArtistResponse> artistApiResponse = await _artistApiMethods.GetArtistAsync(artistId);

            return View(artistApiResponse.Result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] artistIds)
        {

            foreach (int artistId in artistIds)
            {
                ApiResponse<ArtistResponse> artistApiResponse = await _artistApiMethods.DeleteArtistAsync(artistId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (artistApiResponse.HasException)
                {
                    string errorMessage = artistApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Artist") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int artistId)
        {
            ApiResponse<ArtistResponse> artistApiResponse = await _artistApiMethods.GetArtistAsync(artistId);

            ArtistFrontendUpdateResponse artistFrontendResponse = new ArtistFrontendUpdateResponse
            {
                ArtistResponse = artistApiResponse.Result,
                ArtistUpdateRequest = artistApiResponse.Result.ToUpdateRequestDto()
            };

            return View(artistFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int artistId, ArtistFrontendUpdateResponse response)
        {
            ArtistUpdateRequest updateRequest = response.ArtistUpdateRequest;
            ApiResponse<ArtistResponse> artistApiResponse = await _artistApiMethods.UpdateArtistAsync(artistId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (artistApiResponse.HasException)
            {
                string errorMessage = artistApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { artistId = artistApiResponse.Result.ArtistId });
            }
        }
    }
}
