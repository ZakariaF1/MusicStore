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
using System.Linq;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumApiMethods _albumApiMethods;
        public AlbumController(IAlbumApiMethods albumApiMethods)
        {
            _albumApiMethods = albumApiMethods;
        }

        public async Task<IActionResult> GridListAlbums([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<AlbumResponse>> albumsApiResponse = await _albumApiMethods.ListAlbumsAsync();

            IEnumerable<AlbumViewModel> albumsViewModel = albumsApiResponse.Result.Select(album => album.ToViewModel());

            DataSourceResult result = albumsViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> ListAlbums()
        {
            ApiResponse<IEnumerable<AlbumResponse>> albumsApiResponse = await _albumApiMethods.ListAlbumsAsync();

            return Json(albumsApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<AlbumResponse>> albumsApiResponse = await _albumApiMethods.ListAlbumsAsync();

            if (albumsApiResponse.HasException)
            {
                string errorMessage = albumsApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(AlbumCreateRequest request)
        {
            ApiResponse<AlbumResponse> albumApiResponse = await _albumApiMethods.CreateAlbumAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (albumApiResponse.HasException)
            {
                string errorMessage = albumApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int albumId)
        {
            ApiResponse<AlbumResponse> albumApiResponse = await _albumApiMethods.GetAlbumAsync(albumId);

            AlbumViewModel albumViewModel = albumApiResponse.Result.ToViewModel();

            return View(albumViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] albumIds)
        {

            foreach (int albumId in albumIds)
            {
                ApiResponse<AlbumResponse> albumApiResponse = await _albumApiMethods.DeleteAlbumAsync(albumId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (albumApiResponse.HasException)
                {
                    string errorMessage = albumApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Album") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int albumId)
        {
            ApiResponse<AlbumResponse> albumApiResponse = await _albumApiMethods.GetAlbumAsync(albumId);

            AlbumFrontendUpdateResponse albumFrontendResponse = new AlbumFrontendUpdateResponse
            {
                AlbumResponse = albumApiResponse.Result,
                AlbumUpdateRequest = albumApiResponse.Result.ToUpdateRequestDto()
            };

            return View(albumFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int albumId, AlbumFrontendUpdateResponse response)
        {
            AlbumUpdateRequest updateRequest = response.AlbumUpdateRequest;
            ApiResponse<AlbumResponse> albumApiResponse = await _albumApiMethods.UpdateAlbumAsync(albumId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (albumApiResponse.HasException)
            {
                string errorMessage = albumApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { albumId = albumApiResponse.Result.AlbumId });
            }
        }
    }
}
