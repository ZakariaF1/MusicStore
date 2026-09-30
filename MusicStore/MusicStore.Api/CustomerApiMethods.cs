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

    public class CustomerApiMethods : ICustomerApiMethods
    {
        private readonly string _baseUrl;

        public CustomerApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<CustomerResponse>>> ListCustomersAsync()
        {
            string url = $"{_baseUrl}/api/v1/customers";

            ApiResponse<IEnumerable<CustomerResponse>> apiResponse = new ApiResponse<IEnumerable<CustomerResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<CustomerResponse> customers = JsonConvert.DeserializeObject<IEnumerable<CustomerResponse>>(stringJsonResponse);

                    apiResponse.Result = customers;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<CustomerResponse>> CreateCustomerAsync(CustomerCreateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/customers";

            ApiResponse<CustomerResponse> apiResponse = new ApiResponse<CustomerResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                    apiResponse.Result = customer;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<CustomerResponse>> GetCustomerAsync(int customerId)
        {
            string url = $"{_baseUrl}/api/v1/customers/{customerId}";

            ApiResponse<CustomerResponse> apiResponse = new ApiResponse<CustomerResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                    apiResponse.Result = customer;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<CustomerFrontendResponse>> GetCustomerFrontendAsync(int customerId)
        {
            string getCustomerUrl = $"{_baseUrl}/api/v1/customers/{customerId}";
            string listinvoicesUrl = $"{_baseUrl}/api/v1/customers/{customerId}/invoices";
            string listTracksUrl = $"{_baseUrl}/api/v1/customers/{customerId}/tracks";

            ApiResponse<CustomerFrontendResponse> apiResponse = new ApiResponse<CustomerFrontendResponse>();

            CustomerFrontendResponse customerFrontendResponse = new CustomerFrontendResponse();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(getCustomerUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    customerFrontendResponse.CustomerResponse  = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(listinvoicesUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    customerFrontendResponse.InvoiceResponse = JsonConvert.DeserializeObject<IEnumerable<InvoiceResponse>>(stringJsonResponse);
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(listTracksUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    customerFrontendResponse.TrackResponse = JsonConvert.DeserializeObject<IEnumerable<TrackResponse>>(stringJsonResponse);
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            apiResponse.Result = customerFrontendResponse;

            return apiResponse;
        }

        public async Task<ApiResponse<CustomerResponse>> DeleteCustomerAsync(int customerId)
        {
            string url = $"{_baseUrl}/api/v1/customers/{customerId}";

            ApiResponse<CustomerResponse> apiResponse = new ApiResponse<CustomerResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                    apiResponse.Result = customer;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<CustomerResponse>> UpdateCustomerAsync(int customerId, CustomerUpdateRequest request)
        {
            string url = $"{_baseUrl}/api/v1/customers/{customerId}";

            ApiResponse<CustomerResponse> apiResponse = new ApiResponse<CustomerResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                    apiResponse.Result = customer;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<string>> ValidateEmailAddress(int customerId, string email)
        {
            string validateEmailUrl = $"{_baseUrl}/customer/ValidateEmailAddress";

            ApiResponse<string> apiResponse = new ApiResponse<string>();

            if (customerId == 0)
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
                string getCustomerUrl = $"{_baseUrl}/api/v1/customers/{customerId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(getCustomerUrl);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        CustomerResponse customer = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                        if (customer.Email == email)
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
