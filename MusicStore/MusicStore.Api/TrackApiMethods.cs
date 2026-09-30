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

    public class TrackApiMethods : ITrackApiMethods
    {
        private readonly string _baseUrl;

        public TrackApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<TrackResponse>>> ListTracksAsync()
        {
            string url = $"{_baseUrl}/api/v1/tracks";

            ApiResponse<IEnumerable<TrackResponse>> apiResponse = new ApiResponse<IEnumerable<TrackResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<TrackResponse> tracks = JsonConvert.DeserializeObject<IEnumerable<TrackResponse>>(stringJsonResponse);

                    apiResponse.Result = tracks;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<TrackResponse>> CreateTrackAsync(TrackCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/tracks";

            ApiResponse<TrackResponse> apiResponse = new ApiResponse<TrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    TrackResponse track = JsonConvert.DeserializeObject<TrackResponse>(stringJsonResponse);

                    apiResponse.Result = track;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<TrackResponse>> GetTrackAsync(int trackId)
        {
            string url = $"{_baseUrl}/api/v1/tracks/{trackId}";

            ApiResponse<TrackResponse> apiResponse = new ApiResponse<TrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    TrackResponse track = JsonConvert.DeserializeObject<TrackResponse>(stringJsonResponse);

                    apiResponse.Result = track;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<TrackResponse>> DeleteTrackAsync(int trackId)
        {
            string url = $"{_baseUrl}/api/v1/tracks/{trackId}";

            ApiResponse<TrackResponse> apiResponse = new ApiResponse<TrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    TrackResponse track = JsonConvert.DeserializeObject<TrackResponse>(stringJsonResponse);

                    apiResponse.Result = track;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<TrackResponse>> UpdateTrackAsync(int trackId, TrackUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/tracks/{trackId}";

            ApiResponse<TrackResponse> apiResponse = new ApiResponse<TrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    TrackResponse track = JsonConvert.DeserializeObject<TrackResponse>(stringJsonResponse);

                    apiResponse.Result = track;
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
