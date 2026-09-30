using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Api;
using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.ExtensionMethods;
using MusicStoreWebApp.Models.Requests;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApiMethods _customerApiMethods;
        public CustomerController(ICustomerApiMethods customerApiMethods)
        {
            _customerApiMethods = customerApiMethods;
        }

        public async Task<IActionResult> GridListCustomers([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<CustomerResponse>> customersApiResponse = await _customerApiMethods.ListCustomersAsync();

            IEnumerable<CustomerViewModel> customersViewModel = customersApiResponse.Result.Select(customer => customer.ToViewModel());

            DataSourceResult result = customersViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> GridListCustomerInvoices([DataSourceRequest] DataSourceRequest request, int customerId)
        {
            ApiResponse<CustomerFrontendResponse> customerInvoicesApiResponse = await _customerApiMethods.GetCustomerFrontendAsync(customerId);

            IEnumerable<InvoiceViewModel> invoicesViewModel = customerInvoicesApiResponse.Result.InvoiceResponse.Select(invoice => invoice.ToViewModel());

            DataSourceResult result = invoicesViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> GridListCustomerTracks([DataSourceRequest] DataSourceRequest request, int customerId)
        {
            ApiResponse<CustomerFrontendResponse> customerTracksApiResponse = await _customerApiMethods.GetCustomerFrontendAsync(customerId);

            IEnumerable<TrackViewModel> tracksViewModel = customerTracksApiResponse.Result.TrackResponse.Select(track => track.ToViewModel());

            DataSourceResult result = tracksViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> ListCustomers([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<CustomerResponse>> customersApiResponse = await _customerApiMethods.ListCustomersAsync();

            return Json(customersApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<CustomerResponse>> customersApiResponse = await _customerApiMethods.ListCustomersAsync();

            if (customersApiResponse.HasException)
            {
                string errorMessage = customersApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(CustomerCreateRequest request)
        {
            ApiResponse<CustomerResponse> customerApiResponse = await _customerApiMethods.CreateCustomerAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (customerApiResponse.HasException)
            {
                string errorMessage = customerApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int customerId)
        {
            ApiResponse<CustomerResponse> customerApiResponse = await _customerApiMethods.GetCustomerAsync(customerId);

            if (customerApiResponse.HasException)
            {
                string errorMessage = customerApiResponse.Exception.Message;
                return View("Error");
            }
            else
            {
                CustomerViewModel customerViewModel = customerApiResponse.Result.ToViewModel();

                return View(customerViewModel);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] customerIds)
        {

            foreach (int customerId in customerIds)
            {
                ApiResponse<CustomerResponse> customerApiResponse = await _customerApiMethods.DeleteCustomerAsync(customerId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (customerApiResponse.HasException)
                {
                    string errorMessage = customerApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Customer") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int customerId)
        {
            ApiResponse<CustomerResponse> customerApiResponse = await _customerApiMethods.GetCustomerAsync(customerId);

            CustomerFrontendUpdateResponse customerFrontendResponse = new CustomerFrontendUpdateResponse
            {
                CustomerResponse = customerApiResponse.Result,
                CustomerUpdateRequest = customerApiResponse.Result.ToUpdateRequestDto()
            };

            return View(customerFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int customerId, CustomerFrontendUpdateResponse response)
        {
            CustomerUpdateRequest updateRequest = response.CustomerUpdateRequest;
            ApiResponse<CustomerResponse> customerApiResponse = await _customerApiMethods.UpdateCustomerAsync(customerId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (customerApiResponse.HasException)
            {
                string errorMessage = customerApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { customerId = customerApiResponse.Result.CustomerId });
            }
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> ValidateEmailAddress(CheckEmailAddressRequest request)
        {
            ApiResponse<string> stringApiResponse = new ApiResponse<string>();

            request.Email = request.Email.Trim();

            stringApiResponse = await _customerApiMethods.ValidateEmailAddress(request.EntityId, request.Email);

            var result = stringApiResponse.Result.Replace("\"", "");

            return Json(result);
        }
    }
}
