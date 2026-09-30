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
    public class MediaTypeController : Controller
    {
        private readonly IMediaTypeApiMethods _mediaTypeApiMethods;
        public MediaTypeController(IMediaTypeApiMethods mediaTypeApiMethods)
        {
            _mediaTypeApiMethods = mediaTypeApiMethods;
        }

        public async Task<IActionResult> GridListMediaTypes([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<MediaTypeResponse>> mediaTypesApiResponse = await _mediaTypeApiMethods.ListMediaTypesAsync();

            IEnumerable<MediaTypeViewModel> mediaTypesViewModel = mediaTypesApiResponse.Result.Select(mediaType => mediaType.ToViewModel());

            DataSourceResult result = mediaTypesViewModel.ToDataSourceResult(request); ;

            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> ListMediaTypes()
        {
            ApiResponse<IEnumerable<MediaTypeResponse>> mediaTypesApiResponse = await _mediaTypeApiMethods.ListMediaTypesAsync();

            return Json(mediaTypesApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<MediaTypeResponse>> mediaTypesApiResponse = await _mediaTypeApiMethods.ListMediaTypesAsync();

            if (mediaTypesApiResponse.HasException)
            {
                string errorMessage = mediaTypesApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(MediaTypeCreateRequest request)
        {
            ApiResponse<MediaTypeResponse> mediaTypeApiResponse = await _mediaTypeApiMethods.CreateMediaTypeAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (mediaTypeApiResponse.HasException)
            {
                string errorMessage = mediaTypeApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int mediaTypeId)
        {
            ApiResponse<MediaTypeResponse> mediaTypeApiResponse = await _mediaTypeApiMethods.GetMediaTypeAsync(mediaTypeId);

            return View(mediaTypeApiResponse.Result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] mediaTypeIds)
        {

            foreach (int mediaTypeId in mediaTypeIds)
            {
                ApiResponse<MediaTypeResponse> mediaTypeApiResponse = await _mediaTypeApiMethods.DeleteMediaTypeAsync(mediaTypeId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (mediaTypeApiResponse.HasException)
                {
                    string errorMessage = mediaTypeApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "MediaType") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int mediaTypeId)
        {
            ApiResponse<MediaTypeResponse> mediaTypeApiResponse = await _mediaTypeApiMethods.GetMediaTypeAsync(mediaTypeId);

            MediaTypeFrontendUpdateResponse artistFrontendResponse = new MediaTypeFrontendUpdateResponse
            {
                MediaTypeResponse = mediaTypeApiResponse.Result,
                MediaTypeUpdateRequest = mediaTypeApiResponse.Result.ToUpdateRequestDto()
            };

            return View(artistFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int mediaTypeId, MediaTypeFrontendUpdateResponse response)
        {
            MediaTypeUpdateRequest updateRequest = response.MediaTypeUpdateRequest;
            ApiResponse<MediaTypeResponse> mediaTypeApiResponse = await _mediaTypeApiMethods.UpdateMediaTypeAsync(mediaTypeId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (mediaTypeApiResponse.HasException)
            {
                string errorMessage = mediaTypeApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { mediaTypeId = mediaTypeApiResponse.Result.MediaTypeId });
            }
        }
    }
}
