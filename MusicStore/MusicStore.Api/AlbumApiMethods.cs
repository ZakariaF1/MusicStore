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

    public class AlbumApiMethods : IAlbumApiMethods
    {
        private readonly string _baseUrl;

        public AlbumApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<AlbumResponse>>> ListAlbumsAsync()
        {
            string url = $"{_baseUrl}/api/v1/albums";

            ApiResponse<IEnumerable<AlbumResponse>> apiResponse = new ApiResponse<IEnumerable<AlbumResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<AlbumResponse> albums = JsonConvert.DeserializeObject<IEnumerable<AlbumResponse>>(stringJsonResponse);

                    apiResponse.Result = albums;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<AlbumResponse>> CreateAlbumAsync(AlbumCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/albums";

            ApiResponse<AlbumResponse> apiResponse = new ApiResponse<AlbumResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    AlbumResponse album = JsonConvert.DeserializeObject<AlbumResponse>(stringJsonResponse);

                    apiResponse.Result = album;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<AlbumResponse>> GetAlbumAsync(int albumId)
        {
            string url = $"{_baseUrl}/api/v1/albums/{albumId}";

            ApiResponse<AlbumResponse> apiResponse = new ApiResponse<AlbumResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    AlbumResponse album = JsonConvert.DeserializeObject<AlbumResponse>(stringJsonResponse);

                    apiResponse.Result = album;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<AlbumResponse>> DeleteAlbumAsync(int albumId)
        {
            string url = $"{_baseUrl}/api/v1/albums/{albumId}";

            ApiResponse<AlbumResponse> apiResponse = new ApiResponse<AlbumResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    AlbumResponse album = JsonConvert.DeserializeObject<AlbumResponse>(stringJsonResponse);

                    apiResponse.Result = album;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<AlbumResponse>> UpdateAlbumAsync(int albumId, AlbumUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/albums/{albumId}";

            ApiResponse<AlbumResponse> apiResponse = new ApiResponse<AlbumResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    AlbumResponse album = JsonConvert.DeserializeObject<AlbumResponse>(stringJsonResponse);

                    apiResponse.Result = album;
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
