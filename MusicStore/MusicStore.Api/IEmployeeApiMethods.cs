using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IEmployeeApiMethods
    {
        Task<ApiResponse<IEnumerable<EmployeeResponse>>> ListEmployeesAsync();
        Task<ApiResponse<EmployeeResponse>> CreateEmployeeAsync(EmployeeCreateRequest request);
        Task<ApiResponse<EmployeeResponse>> GetEmployeeAsync(int employeeId);
        Task<ApiResponse<EmployeeResponse>> DeleteEmployeeAsync(int employeeId);
        Task<ApiResponse<EmployeeResponse>> UpdateEmployeeAsync(int employeeId, EmployeeUpdateRequest request);
        Task<ApiResponse<string>> ValidateEmailAddress(int employeeId, string email);
    }
}