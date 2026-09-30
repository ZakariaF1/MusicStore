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

    public class ArtistApiMethods : IArtistApiMethods
    {
        private readonly string _baseUrl;

        public ArtistApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<ArtistResponse>>> ListArtistsAsync()
        {
            string url = $"{_baseUrl}/api/v1/artists";

            ApiResponse<IEnumerable<ArtistResponse>> apiResponse = new ApiResponse<IEnumerable<ArtistResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<ArtistResponse> artists = JsonConvert.DeserializeObject<IEnumerable<ArtistResponse>>(stringJsonResponse);

                    apiResponse.Result = artists;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<ArtistResponse>> CreateArtistAsync(ArtistCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/artists";

            ApiResponse<ArtistResponse> apiResponse = new ApiResponse<ArtistResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    ArtistResponse artist = JsonConvert.DeserializeObject<ArtistResponse>(stringJsonResponse);

                    apiResponse.Result = artist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<ArtistResponse>> GetArtistAsync(int artistId)
        {
            string url = $"{_baseUrl}/api/v1/artists/{artistId}";

            ApiResponse<ArtistResponse> apiResponse = new ApiResponse<ArtistResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    ArtistResponse artist = JsonConvert.DeserializeObject<ArtistResponse>(stringJsonResponse);

                    apiResponse.Result = artist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<ArtistResponse>> DeleteArtistAsync(int artistId)
        {
            string url = $"{_baseUrl}/api/v1/artists/{artistId}";

            ApiResponse<ArtistResponse> apiResponse = new ApiResponse<ArtistResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    ArtistResponse artist = JsonConvert.DeserializeObject<ArtistResponse>(stringJsonResponse);

                    apiResponse.Result = artist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<ArtistResponse>> UpdateArtistAsync(int artistId, ArtistUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/artists/{artistId}";

            ApiResponse<ArtistResponse> apiResponse = new ApiResponse<ArtistResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    ArtistResponse artist = JsonConvert.DeserializeObject<ArtistResponse>(stringJsonResponse);

                    apiResponse.Result = artist;
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
