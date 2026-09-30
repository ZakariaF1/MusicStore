using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> EmployeeBaseQuery();
        Task<IEnumerable<Employee>> ListEmployeesAsync(IQueryable<Employee> query, bool asNoTracking = false);
        Task CreateEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeAsync(int employeeId);
        void DeleteEmployee(Employee employee);
        Task<bool> EmployeeExistsAsync(int? employeeId);
        Task<bool> EmailAddressExistsAsync(string email);
        Task SaveAsync();
    }
}
