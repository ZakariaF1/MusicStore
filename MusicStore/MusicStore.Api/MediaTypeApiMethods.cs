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

    public class MediaTypeApiMethods : IMediaTypeApiMethods
    {
        private readonly string _baseUrl;

        public MediaTypeApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<MediaTypeResponse>>> ListMediaTypesAsync()
        {
            string url = $"{_baseUrl}/api/v1/mediaTypes";

            ApiResponse<IEnumerable<MediaTypeResponse>> apiResponse = new ApiResponse<IEnumerable<MediaTypeResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<MediaTypeResponse> mediaTypes = JsonConvert.DeserializeObject<IEnumerable<MediaTypeResponse>>(stringJsonResponse);

                    apiResponse.Result = mediaTypes;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<MediaTypeResponse>> CreateMediaTypeAsync(MediaTypeCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/mediaTypes";

            ApiResponse<MediaTypeResponse> apiResponse = new ApiResponse<MediaTypeResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    MediaTypeResponse mediaType = JsonConvert.DeserializeObject<MediaTypeResponse>(stringJsonResponse);

                    apiResponse.Result = mediaType;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<MediaTypeResponse>> GetMediaTypeAsync(int mediaTypeId)
        {
            string url = $"{_baseUrl}/api/v1/mediaTypes/{mediaTypeId}";

            ApiResponse<MediaTypeResponse> apiResponse = new ApiResponse<MediaTypeResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    MediaTypeResponse mediaType = JsonConvert.DeserializeObject<MediaTypeResponse>(stringJsonResponse);

                    apiResponse.Result = mediaType;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<MediaTypeResponse>> DeleteMediaTypeAsync(int mediaTypeId)
        {
            string url = $"{_baseUrl}/api/v1/mediaTypes/{mediaTypeId}";

            ApiResponse<MediaTypeResponse> apiResponse = new ApiResponse<MediaTypeResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    MediaTypeResponse mediaType = JsonConvert.DeserializeObject<MediaTypeResponse>(stringJsonResponse);

                    apiResponse.Result = mediaType;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<MediaTypeResponse>> UpdateMediaTypeAsync(int mediaTypeId, MediaTypeUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/mediaTypes/{mediaTypeId}";

            ApiResponse<MediaTypeResponse> apiResponse = new ApiResponse<MediaTypeResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    MediaTypeResponse mediaType = JsonConvert.DeserializeObject<MediaTypeResponse>(stringJsonResponse);

                    apiResponse.Result = mediaType;
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
