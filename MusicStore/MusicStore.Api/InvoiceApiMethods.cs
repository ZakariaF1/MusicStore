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

    public class InvoiceApiMethods : IInvoiceApiMethods
    {
        private readonly string _baseUrl;

        public InvoiceApiMethods(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<ApiResponse<IEnumerable<InvoiceResponse>>> ListInvoicesAsync()
        {
            string url = $"{_baseUrl}/api/v1/invoices";

            ApiResponse<IEnumerable<InvoiceResponse>> apiResponse = new ApiResponse<IEnumerable<InvoiceResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<InvoiceResponse> invoices = JsonConvert.DeserializeObject<IEnumerable<InvoiceResponse>>(stringJsonResponse);

                    apiResponse.Result = invoices;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<InvoiceResponse>> CreateInvoiceAsync(InvoiceFrontendCreateRequest request)
        {
            int invoiceId = 0;
            string createInvoiceUrl = $"{_baseUrl}/api/v1/invoices";

            ApiResponse<InvoiceResponse> apiResponse = new ApiResponse<InvoiceResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request.InvoiceCreateRequest);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PostAsync(createInvoiceUrl, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    InvoiceResponse invoice = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);

                    apiResponse.Result = invoice;

                    invoiceId = invoice.InvoiceId;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);

                }
            }

            string createInvoiceItemUrl = $"{_baseUrl}/api/v1/invoices/{invoiceId}/invoiceItems";

            using (HttpClient client = new HttpClient())
            {

                foreach (InvoiceItemCreateRequest invoiceItem in request.InvoiceItemCreateRequests)
                {
                    string json = JsonConvert.SerializeObject(invoiceItem);

                    StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    HttpResponseMessage httpResponseMessage = await client.PostAsync(createInvoiceItemUrl, content);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }

            return apiResponse;
        }

        //public async Task<ApiResponse<InvoiceFrontendResponse>> GetInvoiceFrontendAsync(int invoiceId, int customerId)
        //{
        //    string getInvoiceUrl = $"{_baseUrl}/api/v1/invoices/{invoiceId}";
        //    string getCustomerUrl = $"{_baseUrl}/api/v1/customers/{customerId}";

        //    ApiResponse<InvoiceFrontendResponse> apiResponse = new ApiResponse<InvoiceFrontendResponse>();

        //    InvoiceFrontendResponse invoiceFrontendResponse = new InvoiceFrontendResponse();

        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage httpResponseMessage = await client.GetAsync(getInvoiceUrl);

        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

        //            invoiceFrontendResponse.InvoiceResponse = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);
        //        }
        //        else
        //        {
        //            apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage httpResponseMessage = await client.GetAsync(getCustomerUrl);

        //        if (httpResponseMessage.IsSuccessStatusCode)
        //        {
        //            string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

        //            invoiceFrontendResponse.CustomerResponse = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);
        //        }
        //        else
        //        {
        //            apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
        //        }
        //    }
        //    apiResponse.Result = invoiceFrontendResponse;

        //    return apiResponse;
        //}
        public async Task<Tuple<ApiResponse<InvoiceResponse>, ApiResponse<CustomerResponse>>> GetInvoiceFrontendAsync(int invoiceId, int customerId)
        {
            string getInvoiceUrl = $"{_baseUrl}/api/v1/invoices/{invoiceId}";
            string getCustomerUrl = $"{_baseUrl}/api/v1/customers/{customerId}";

            ApiResponse<InvoiceResponse> invoiceApiResponse = new ApiResponse<InvoiceResponse>();
            ApiResponse<CustomerResponse> customerApiResponse = new ApiResponse<CustomerResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(getInvoiceUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    InvoiceResponse invoiceResponse = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);

                    invoiceApiResponse.Result = invoiceResponse;
                }
                else
                {
                    invoiceApiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(getCustomerUrl);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    CustomerResponse customerResponse = JsonConvert.DeserializeObject<CustomerResponse>(stringJsonResponse);

                    customerApiResponse.Result = customerResponse;
                }
                else
                {
                    customerApiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }

            return Tuple.Create(invoiceApiResponse, customerApiResponse);
        }

        public async Task<ApiResponse<IEnumerable<InvoiceItemResponse>>> ListInvoiceItemsAsync(int invoiceId)
        {
            string url = $"{_baseUrl}/api/v1/invoices/{invoiceId}/invoiceItems";

            ApiResponse<IEnumerable<InvoiceItemResponse>> apiResponse = new ApiResponse<IEnumerable<InvoiceItemResponse>>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    IEnumerable<InvoiceItemResponse> invoiceItems = JsonConvert.DeserializeObject<IEnumerable<InvoiceItemResponse>>(stringJsonResponse);

                    apiResponse.Result = invoiceItems;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<InvoiceResponse>> DeleteInvoiceAsync(int invoiceId)
        {
            string url = $"{_baseUrl}/api/v1/invoices/{invoiceId}";

            ApiResponse<InvoiceResponse> apiResponse = new ApiResponse<InvoiceResponse>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    InvoiceResponse invoice = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);

                    apiResponse.Result = invoice;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            return apiResponse;
        }

        public async Task<ApiResponse<InvoiceResponse>> UpdateInvoiceAsync(InvoiceFrontendUpdateRequest request)
        {
            string updateInvoiceUrl = $"{_baseUrl}/api/v1/invoices/{request.InvoiceId}";
            string createInvoiceItemUrl = $"{_baseUrl}/api/v1/invoices/{request.InvoiceId}/invoiceItems";

            ApiResponse<InvoiceResponse> apiResponse = new ApiResponse<InvoiceResponse>();

            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(request.InvoiceUpdateRequest);

                StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await client.PutAsync(updateInvoiceUrl, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                    InvoiceResponse invoice = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);

                    apiResponse.Result = invoice;
                }
                else
                {
                    apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                }
            }
            using (HttpClient client = new HttpClient())
            {
                foreach (InvoiceItemCreateRequest invoiceItemCreateRequest in request.InvoiceItemCreateRequests)
                {
                    string json = JsonConvert.SerializeObject(invoiceItemCreateRequest);

                    StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    HttpResponseMessage httpResponseMessage = await client.PostAsync(createInvoiceItemUrl, content);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string stringJsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                        InvoiceResponse invoice = JsonConvert.DeserializeObject<InvoiceResponse>(stringJsonResponse);

                        apiResponse.Result = invoice;
                    }
                    else
                    {
                        apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            using (HttpClient client = new HttpClient())
            {
                if (request.InvoiceItemUpdateRequestIds.Length > 0)
                {
                    for (int i = 0; i < request.InvoiceItemUpdateRequestIds.Length; i++)
                    {
                        string url = $"{_baseUrl}/api/v1/invoices/{request.InvoiceId}/invoiceItems/{request.InvoiceItemUpdateRequestIds[i]}";

                        string json = JsonConvert.SerializeObject(request.InvoiceItemUpdateRequests[i]);

                        StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                        HttpResponseMessage httpResponseMessage = await client.PutAsync(url, content);

                        if (!httpResponseMessage.IsSuccessStatusCode)
                        {
                            apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                        }
                    }
                }
            }
            using (HttpClient client = new HttpClient())
            {
                foreach (int invoiceLineId in request.InvoiceItemsForDeletion)
                {
                    string url = $"{_baseUrl}/api/v1/invoices/{request.InvoiceId}/invoiceItems/{invoiceLineId}";

                    HttpResponseMessage httpResponseMessage = await client.DeleteAsync(url);

                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        apiResponse.Exception = new Exception(httpResponseMessage.ReasonPhrase);
                    }
                }
            }
            return apiResponse;
        }
    }
}
