using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Api;
using MusicStore.Api.Requests;
using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.ExtensionMethods;
using MusicStoreWebApp.Models.Requests;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeApiMethods _employeeApiMethods;
        public EmployeeController(IEmployeeApiMethods employeeApiMethods)
        {
            _employeeApiMethods = employeeApiMethods;
        }

        public async Task<IActionResult> GridListEmployees([DataSourceRequest] DataSourceRequest request)
        {
            ApiResponse<IEnumerable<EmployeeResponse>> employeesApiResponse = await _employeeApiMethods.ListEmployeesAsync();

            IEnumerable<EmployeeResponse> employeesResponse = employeesApiResponse.Result;

            List<EmployeeViewModel> employeesViewModel = new List<EmployeeViewModel>();

            foreach (EmployeeResponse employeeResponse in employeesResponse)
            {
                if (employeeResponse.ReportsTo == null)
                {
                    EmployeeViewModel employeeViewModel = employeeResponse.ToViewModel();

                    employeesViewModel.Add(employeeViewModel);
                }
                else
                {
                    EmployeeResponse reportsToEmployee = employeesResponse.Where(employee => employee.EmployeeId == employeeResponse.ReportsTo).FirstOrDefault();

                    EmployeeViewModel employeeViewModel = employeeResponse.ToViewModel(reportsToEmployee.FullName);

                    employeesViewModel.Add(employeeViewModel);
                }
            }

            DataSourceResult result = employeesViewModel.ToDataSourceResult(request);

            return Json(result);
        }

        public async Task<IActionResult> ListEmployees()
        {
            ApiResponse<IEnumerable<EmployeeResponse>> employeesApiResponse = await _employeeApiMethods.ListEmployeesAsync();

            return Json(employeesApiResponse.Result);
        }

        public async Task<ActionResult> Index()
        {
            ApiResponse<IEnumerable<EmployeeResponse>> employeesApiResponse = await _employeeApiMethods.ListEmployeesAsync(); //this is actually the ApiResponse<Ienu<Empl....

            if (employeesApiResponse.HasException)
            {
                string errorMessage = employeesApiResponse.Exception.Message;
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
        public async Task<ActionResult> Create(EmployeeCreateRequest request)
        {
            ApiResponse<EmployeeResponse> employeeApiResponse = await _employeeApiMethods.CreateEmployeeAsync(request);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (employeeApiResponse.HasException)
            {
                string errorMessage = employeeApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Index", new { lastGridPage = "true" });
            }
        }

        public async Task<ActionResult> Details(int employeeId)
        {
            ApiResponse<EmployeeResponse> employeeApiResponse = await _employeeApiMethods.GetEmployeeAsync(employeeId);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel();

            if (employeeApiResponse.Result.ReportsTo == null)
            {
                employeeViewModel = employeeApiResponse.Result.ToViewModel();
            }
            else
            {
                ApiResponse<EmployeeResponse> reportsToEmployeeApiResponse = await _employeeApiMethods.GetEmployeeAsync((int)employeeApiResponse.Result.ReportsTo);
                employeeViewModel = employeeApiResponse.Result.ToViewModel(reportsToEmployeeApiResponse.Result.FullName);
            }

            return View(employeeViewModel);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] int[] employeeIds)
        {

            foreach (int employeeId in employeeIds)
            {
                ApiResponse<EmployeeResponse> employeeApiResponse = await _employeeApiMethods.DeleteEmployeeAsync(employeeId);

                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (employeeApiResponse.HasException)
                {
                    string errorMessage = employeeApiResponse.Exception.Message;
                    ///return nice error to UI 
                    return View("Error");
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Employee") });
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int employeeId)
        {
            ApiResponse<EmployeeResponse> employeeApiResponse = await _employeeApiMethods.GetEmployeeAsync(employeeId);

            EmployeeFrontendUpdateResponse employeeFrontendResponse = new EmployeeFrontendUpdateResponse
            {
                EmployeeResponse = employeeApiResponse.Result,
                EmployeeUpdateRequest = employeeApiResponse.Result.ToUpdateRequestDto()
            };

            return View(employeeFrontendResponse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int employeeId, EmployeeFrontendUpdateResponse response)
        {
            EmployeeUpdateRequest updateRequest = response.EmployeeUpdateRequest;
            ApiResponse<EmployeeResponse> employeeApiResponse = await _employeeApiMethods.UpdateEmployeeAsync(employeeId, updateRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (employeeApiResponse.HasException)
            {
                string errorMessage = employeeApiResponse.Exception.Message;
                ///return nice error to UI 
                return View("Error");
            }
            else
            {
                return RedirectToAction("Details", new { employeeId = employeeApiResponse.Result.EmployeeId });
            }
        }

        [HttpGet]
        public ActionResult ListCountries()
        {
            List<string> countries = new List<string>();
            foreach (CountriesEnum.Countries country in Enum.GetValues(typeof(CountriesEnum.Countries)))
            {
                countries.Add(CountriesEnum.StringValueOfEnum(country));
            }
            return Json(countries);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<ActionResult> ValidateEmailAddress(CheckEmailAddressRequest request)
        {
            ApiResponse<string> stringApiResponse = new ApiResponse<string>();

            request.Email = request.Email.Trim();

            stringApiResponse = await _employeeApiMethods.ValidateEmailAddress(request.EntityId, request.Email);

            var result = stringApiResponse.Result.Replace("\"", "");

            return Json(result);
        }
    }
}
