using Microsoft.AspNetCore.Mvc;
using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using MusicStore.Repository;
using MusicStore.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicStore.Services;

namespace MusicStore.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet("api/v1/employees")]
        public async Task<IActionResult> ListEmployees()
        {
            IQueryable<Employee> employeesQuery = _employeeRepository.EmployeeBaseQuery();

            employeesQuery = employeesQuery.OrderBy(employee => employee.EmployeeId);

            IEnumerable<Employee> employees = await _employeeRepository.ListEmployeesAsync(employeesQuery, true);

            IEnumerable<EmployeeResponse> response = employees.Select(employee => employee.ToResponseDto());

            return Ok(response);
        }

        [HttpPost("api/v1/employees")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<Employee> employeesQuery = _employeeRepository.EmployeeBaseQuery();

            bool employeeExists = await _employeeRepository.EmployeeExistsAsync(request.ReportsTo);

            if (employeeExists || request.ReportsTo.HasValue == false)
            {
                Employee employee = request.ToEntity();

                await _employeeRepository.CreateEmployeeAsync(employee);

                await _employeeRepository.SaveAsync();

                EmployeeResponse response = employee.ToResponseDto();

                return CreatedAtRoute("GetEmployeeById", new { employeeId = employee.EmployeeId }, response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet("api/v1/employees/{employeeId}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployee([FromRoute] int employeeId)
        {
            IQueryable<Employee> employeesQuery = _employeeRepository.EmployeeBaseQuery();

            Employee employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (employee == null)
            {
                return NotFound("Employee wasn't found");
            }
            else
            {
                EmployeeResponse response = employee.ToResponseDto();
                return Ok(response);
            }
        }

        [HttpDelete("api/v1/employees/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int employeeId)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (employee == null)
            {
                return NotFound("Employee wasn't found");
            }
            else
            {
                //_employeeRepository.DeleteEmployee(employee);
                await _employeeService.DeleteEmployeeCascadeAsync(employee);
                await _employeeRepository.SaveAsync();
                return NoContent();
            }
        }

        [HttpPut("api/v1/employees/{employeeId}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int employeeId, [FromBody] EmployeeUpdateRequest request)
        {
            Employee employee = await _employeeRepository.GetEmployeeAsync(employeeId);
            if (employee == null)
            {
                return NotFound("Employee wasn't found");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool employeeExists = await _employeeRepository.EmployeeExistsAsync(request.ReportsTo);

            if (employeeExists || request.ReportsTo.HasValue == false)
            {
                employee.UpdateEmployee(request);
                await _employeeRepository.SaveAsync();

                EmployeeResponse response = employee.ToResponseDto();

                return Ok(response);
            }
            else
            {
                return BadRequest("The accountable employee dont exist, please change ReportsTo property");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ValidateEmailAddress([FromBody] string email)
        {
            bool emailExists = await _employeeRepository.EmailAddressExistsAsync(email);

            if (emailExists)
            {
                return Json("This email is used by an another employee");
            }
            else
            {
                return Json(true);
            }
        }
    }
}
