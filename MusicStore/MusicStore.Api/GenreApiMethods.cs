using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Api
{

    public class GenreApiMethods : IGenreApiMethods
    {
        private readonly string _baseUrl;

        public GenreApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<GenreResponse>>> ListGenresAsync()
        {
            string url = $"{_baseUrl}/api/v1/genres";

            ApiResponse<IEnumerable<GenreResponse>> apiResponse = new ApiResponse<IEnumerable<GenreResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<GenreResponse> genres = JsonConvert.DeserializeObject<IEnumerable<GenreResponse>>(stringJsonResponse);

                    apiResponse.Result = genres;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<GenreResponse>> CreateGenreAsync(GenreCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/genres";

            ApiResponse<GenreResponse> apiResponse = new ApiResponse<GenreResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    GenreResponse genre = JsonConvert.DeserializeObject<GenreResponse>(stringJsonResponse);

                    apiResponse.Result = genre;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<GenreResponse>> GetGenreAsync(int genreId)
        {
            string url = $"{_baseUrl}/api/v1/genres/{genreId}";

            ApiResponse<GenreResponse> apiResponse = new ApiResponse<GenreResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    GenreResponse genre = JsonConvert.DeserializeObject<GenreResponse>(stringJsonResponse);

                    apiResponse.Result = genre;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<GenreResponse>> DeleteGenreAsync(int genreId)
        {
            string url = $"{_baseUrl}/api/v1/genres/{genreId}";

            ApiResponse<GenreResponse> apiResponse = new ApiResponse<GenreResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    GenreResponse genre = JsonConvert.DeserializeObject<GenreResponse>(stringJsonResponse);

                    apiResponse.Result = genre;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<GenreResponse>> UpdateGenreAsync(int genreId, GenreUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/genres/{genreId}";

            ApiResponse<GenreResponse> apiResponse = new ApiResponse<GenreResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    GenreResponse genre = JsonConvert.DeserializeObject<GenreResponse>(stringJsonResponse);

                    apiResponse.Result = genre;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }
    }
}
