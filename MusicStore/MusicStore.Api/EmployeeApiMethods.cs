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

    public class EmployeeApiMethods : IEmployeeApiMethods
    {
        private readonly string _baseUrl;

        public EmployeeApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeResponse>>> ListEmployeesAsync()
        {
            string url = $"{_baseUrl}/api/v1/employees";

            ApiResponse<IEnumerable<EmployeeResponse>> apiResponse = new ApiResponse<IEnumerable<EmployeeResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<EmployeeResponse> employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeResponse>>(stringJsonResponse);

                    apiResponse.Result = employees;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<EmployeeResponse>> CreateEmployeeAsync(EmployeeCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/employees";

            ApiResponse<EmployeeResponse> apiResponse = new ApiResponse<EmployeeResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(stringJsonResponse);

                    apiResponse.Result = employee;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<EmployeeResponse>> GetEmployeeAsync(int employeeId)
        {
            string url = $"{_baseUrl}/api/v1/employees/{employeeId}";

            ApiResponse<EmployeeResponse> apiResponse = new ApiResponse<EmployeeResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(stringJsonResponse);

                    apiResponse.Result = employee;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);  //mapping businessexception to exception //new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<EmployeeResponse>> DeleteEmployeeAsync(int employeeId)
        {
            string url = $"{_baseUrl}/api/v1/employees/{employeeId}";

            ApiResponse<EmployeeResponse> apiResponse = new ApiResponse<EmployeeResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(stringJsonResponse);

                    apiResponse.Result = employee;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<EmployeeResponse>> UpdateEmployeeAsync(int employeeId, EmployeeUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/employees/{employeeId}";

            ApiResponse<EmployeeResponse> apiResponse = new ApiResponse<EmployeeResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(stringJsonResponse);

                    apiResponse.Result = employee;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<string>> ValidateEmailAddress(int employeeId, string email)
        {
            string validateEmailUrl = $"{_baseUrl}/employee/ValidateEmailAddress";

            ApiResponse<string> apiResponse = new ApiResponse<string>();

            if (employeeId == 0)
            {
                using (HttpClient client = new HttpClient())
                {

                    string json = JsonConvert.SerializeObject(email);

                    StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    HttpResponseMessage httpResponseMessage = await client.PostAsync(validateEmailUrl, content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        apiResponse.Result = stringJsonResponse;
                    }
                    else
                    {
                        apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            else
            {
                string getEmployeeUrl = $"{_baseUrl}/api/v1/employees/{employeeId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(getEmployeeUrl);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        EmployeeResponse employee = JsonConvert.DeserializeObject<EmployeeResponse>(stringJsonResponse);

                        if (employee.Email == email)
                        {
                            apiResponse.Result = "true";
                        }
                        else
                        {
                            using (HttpClient client2 = new HttpClient())
                            {
                                string json = JsonConvert.SerializeObject(email);

                                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                                HttpResponseMessage httpResponseMessage2 = await client2.PostAsync(validateEmailUrl, content);

                                if (httpResponseMessage2.IsSuccessStatusCode)
                                {
                                    string stringJsonResponse2 = await httpResponseMessage2.Content.ReadAsStringAsync();

                                    apiResponse.Result = stringJsonResponse2;
                                }
                                else
                                {
                                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                                }
                            }
                        }
                    }
                    else
                    {
                        apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            return apiResponse;
        }
    }
}
