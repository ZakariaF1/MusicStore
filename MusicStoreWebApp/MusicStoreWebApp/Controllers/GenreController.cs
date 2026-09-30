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
    public class GenreController : Controller
    {
        private readonly IGenreApiMethods _genreApiMethods;
        public GenreController(IGenreApiMethods genreApiMethods)
        {
            _genreApiMethods = genreApiMethods;
        }

        public async Task<IActionResult> GridListGenres([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<GenreResponse>> genresApiResponse = await _genreApiMethods.ListGenresAsync();

            IEnumerable<GenreViewModel> genresViewModel = genresApiResponse.Result.Select(genre => genre.ToViewModel());

            DataSourceResult result = genresViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        [HttpGet]
        public async Task<ActionResult> ListGenres()
        {
            ApiResponse<IEnumerable<GenreResponse>> genresApiResponse = await _genreApiMethods.ListGenresAsync();

            return Json(genresApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<GenreResponse>> genresApiResponse = await _genreApiMethods.ListGenresAsync();

            if (genresApiResponse.HasException)
            {
                string errorMessage = genresApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(GenreCreateRequest request)
        {
            ApiResponse<GenreResponse> genreApiResponse = await _genreApiMethods.CreateGenreAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (genreApiResponse.HasException)
            {
                string errorMessage = genreApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int genreId)
        {
            ApiResponse<GenreResponse> genreApiResponse = await _genreApiMethods.GetGenreAsync(genreId);

            return View(genreApiResponse.Result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] genreIds)
        {

            foreach (int genreId in genreIds)
            {
                ApiResponse<GenreResponse> genreApiResponse = await _genreApiMethods.DeleteGenreAsync(genreId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (genreApiResponse.HasException)
                {
                    string errorMessage = genreApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Genre") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int genreId)
        {
            ApiResponse<GenreResponse> genreApiResponse = await _genreApiMethods.GetGenreAsync(genreId);

            GenreFrontendUpdateResponse genreFrontendResponse = new GenreFrontendUpdateResponse
            {
                GenreResponse = genreApiResponse.Result,
                GenreUpdateRequest = genreApiResponse.Result.ToUpdateRequestDto()
            };

            return View(genreFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int genreId, GenreFrontendUpdateResponse response)
        {
            GenreUpdateRequest updateRequest = response.GenreUpdateRequest;
            ApiResponse<GenreResponse> genreApiResponse = await _genreApiMethods.UpdateGenreAsync(genreId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (genreApiResponse.HasException)
            {
                string errorMessage = genreApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return View("Details", genreApiResponse.Result);

            }
        }
    }
}
