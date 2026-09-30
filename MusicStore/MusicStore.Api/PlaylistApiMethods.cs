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

    public class PlaylistApiMethods : IPlaylistApiMethods
    {
        private readonly string _baseUrl;

        public PlaylistApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<PlaylistResponse>>> ListPlaylistsAsync()
        {
            string url = $"{_baseUrl}/api/v1/playlists";

            ApiResponse<IEnumerable<PlaylistResponse>> apiResponse = new ApiResponse<IEnumerable<PlaylistResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<PlaylistResponse> playlists = JsonConvert.DeserializeObject<IEnumerable<PlaylistResponse>>(stringJsonResponse);

                    apiResponse.Result = playlists;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistResponse>> CreatePlaylistAsync(PlaylistCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/playlists";

            ApiResponse<PlaylistResponse> apiResponse = new ApiResponse<PlaylistResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistResponse playlist = JsonConvert.DeserializeObject<PlaylistResponse>(stringJsonResponse);

                    apiResponse.Result = playlist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistResponse>> GetPlaylistAsync(int playlistId)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}";

            ApiResponse<PlaylistResponse> apiResponse = new ApiResponse<PlaylistResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistResponse playlist = JsonConvert.DeserializeObject<PlaylistResponse>(stringJsonResponse);

                    apiResponse.Result = playlist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistResponse>> DeletePlaylistAsync(int playlistId)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}";

            ApiResponse<PlaylistResponse> apiResponse = new ApiResponse<PlaylistResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistResponse playlist = JsonConvert.DeserializeObject<PlaylistResponse>(stringJsonResponse);

                    apiResponse.Result = playlist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistResponse>> UpdatePlaylistAsync(int playlistId, PlaylistUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}";

            ApiResponse<PlaylistResponse> apiResponse = new ApiResponse<PlaylistResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistResponse playlist = JsonConvert.DeserializeObject<PlaylistResponse>(stringJsonResponse);

                    apiResponse.Result = playlist;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<IEnumerable<PlaylistTrackResponse>>> ListPlaylistTracksAsync(int playlistId)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}/playlistTracks";

            ApiResponse<IEnumerable<PlaylistTrackResponse>> apiResponse = new ApiResponse<IEnumerable<PlaylistTrackResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<PlaylistTrackResponse> playlistTracks = JsonConvert.DeserializeObject<IEnumerable<PlaylistTrackResponse>>(stringJsonResponse);

                    apiResponse.Result = playlistTracks;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistTrackResponse>> CreatePlaylistTrackAsync(int playlistId, PlaylistTrackCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}/playlistTracks";

            ApiResponse<PlaylistTrackResponse> apiResponse = new ApiResponse<PlaylistTrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistTrackResponse playlistTrack = JsonConvert.DeserializeObject<PlaylistTrackResponse>(stringJsonResponse);

                    apiResponse.Result = playlistTrack;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<PlaylistTrackResponse>> DeletePlaylistTrackAsync(int playlistId, int playlistTrackId)
        {
            string url = $"{_baseUrl}/api/v1/playlists/{playlistId}/playlistTracks/{playlistTrackId}";

            ApiResponse<PlaylistTrackResponse> apiResponse = new ApiResponse<PlaylistTrackResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    PlaylistTrackResponse playlistTrack = JsonConvert.DeserializeObject<PlaylistTrackResponse>(stringJsonResponse);

                    apiResponse.Result = playlistTrack;
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
