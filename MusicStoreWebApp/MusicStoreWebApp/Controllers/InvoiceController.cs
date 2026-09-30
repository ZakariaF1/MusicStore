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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStoreWebApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceApiMethods _invoiceApiMethods;
        public InvoiceController(IInvoiceApiMethods invoiceApiMethods)
        {
            _invoiceApiMethods = invoiceApiMethods;
        }

        public async Task<IActionResult> GridListInvoices([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<InvoiceResponse>> invoicesApiResponse = await _invoiceApiMethods.ListInvoicesAsync();

            IEnumerable<InvoiceViewModel> invoicesViewModel = invoicesApiResponse.Result.Select(invoice => invoice.ToViewModel());

            DataSourceResult result = invoicesViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> GridListInvoiceItems([DataSourceRequest] DataSourceRequest request, int invoiceId)
        {
            ApiResponse<IEnumerable<InvoiceItemResponse>> invoiceInvoiceItemsApiResponse = await _invoiceApiMethods.ListInvoiceItemsAsync(invoiceId);

            IEnumerable<InvoiceItemViewModel> invoiceItemsViewModel = invoiceInvoiceItemsApiResponse.Result.Select(invoiceItem => invoiceItem.ToViewModel());

            DataSourceResult result = invoiceItemsViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<InvoiceResponse>> invoicesApiResponse = await _invoiceApiMethods.ListInvoicesAsync();

            if (invoicesApiResponse.HasException)
            {
                string errorMessage = invoicesApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create([FromBody] InvoiceFrontendCreateRequest request)
        {
            ApiResponse<InvoiceResponse> customerApiResponse = await _invoiceApiMethods.CreateInvoiceAsync(request);

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
                if (request.IsFromCustomer)
                {
                    return Json(new { redirectToUrl = Url.Action("Details", "Customer", new { customerId = request.CustomerId, lastGridPage = "true" }) });
                }
                else
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Invoice", new { lastGridPage = "true" }) });
                }
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] InvoiceDeleteRequest request)
        {

            foreach (int invoiceId in request.InvoiceIdsForDeletion)
            {
                ApiResponse<InvoiceResponse> invoiceApiResponse = await _invoiceApiMethods.DeleteInvoiceAsync(invoiceId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (invoiceApiResponse.HasException)
                {
                    string errorMessage = invoiceApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            if (request.IsFromCustomer)
            {
                return Json(new { redirectToUrl = Url.Action("Details", "Customer", new { customerId = request.CustomerId }) });
            }
            else
            {
                return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
            }
        }

        //[HttpDelete]
        //public async Task<ActionResult> DeleteFromCustomer([FromBody] int[] invoiceIds)
        //{

        //    foreach (int invoiceId in invoiceIds)
        //    {
        //        ApiResponse<InvoiceResponse> invoiceApiResponse = await _invoiceApiMethods.DeleteInvoiceAsync(invoiceId);

        //        if (!ModelState.IsValid)
        //        {
        //            return View();
        //        }

        //        if (invoiceApiResponse.HasException)
        //        {
        //            string errorMessage = invoiceApiResponse.Exception.Message;
        //            ///return nice error to UI 
        //            return View("Error");
        //        }
        //    }
        //    return Json(new { redirectToUrl = Url.Action("Details", "Invoice") });
        //}

        [HttpGet]
        public async Task<ActionResult> Edit(int invoiceId, int customerId)
        {
            Tuple<ApiResponse<InvoiceResponse>, ApiResponse<CustomerResponse>> invoiceFrontendApiResponse = await _invoiceApiMethods.GetInvoiceFrontendAsync(invoiceId, customerId);

            //InvoiceFrontendUpdateResponse invoiceFrontendUpdateResponse = new InvoiceFrontendUpdateResponse
            //{
            //    InvoiceResponse = invoiceFrontendApiResponse.Result.InvoiceResponse,
            //    InvoiceUpdateRequest = invoiceFrontendApiResponse.Result.InvoiceResponse.ToUpdateRequestDto(),
            //    CustomerResponse = invoiceFrontendApiResponse.Result.CustomerResponse
            //};

            InvoiceFrontendUpdateResponse invoiceFrontendUpdateResponse = new InvoiceFrontendUpdateResponse
            {
                InvoiceResponse = invoiceFrontendApiResponse.Item1.Result,
                InvoiceUpdateRequest = invoiceFrontendApiResponse.Item1.Result.ToUpdateRequestDto(),
                CustomerResponse = invoiceFrontendApiResponse.Item2.Result
            };

            return View(invoiceFrontendUpdateResponse);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromBody]InvoiceFrontendUpdateRequest request)
        {
            ApiResponse<InvoiceResponse> invoiceApiResponse = await _invoiceApiMethods.UpdateInvoiceAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (invoiceApiResponse.HasException)
            {
                string errorMessage = invoiceApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                if (request.IsFromCustomer)
                {
                    return Json(new { redirectToUrl = Url.Action("Details", "Customer", new { customerId = request.CustomerId }) });
                }
                else
                {
                    return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
                }
            }
        }
    }
}
