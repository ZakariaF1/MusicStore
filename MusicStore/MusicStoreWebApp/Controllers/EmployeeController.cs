using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Api.Requests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using MusicStore.Repository;
using MusicStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

  
        public async Task<IActionResult> EmployeeIndex([DataSourceRequest]DataSourceRequest request)
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesAsync();

            DataSourceResult dataSource = employees.Select(employee => employee.ToResponseDto()).ToDataSourceResult(request);

            return Json(dataSource);
        }

        //public async Task<IActionResult> GetEmployees([DataSourceRequest] DataSourceRequest request)
        //{

        //    IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesAsync();

        //    IEnumerable<EmployeeResponse> response = employees.Select(employee => employee.ToResponseDto());

        //    return View(response);
        //}
    }
}
