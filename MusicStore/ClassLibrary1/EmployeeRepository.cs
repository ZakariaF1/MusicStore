using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public EmployeeRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Employee> EmployeeBaseQuery()
        {
            IQueryable<Employee> employeesQuery = _unitOfWork.Employees;
            return employeesQuery;
        }

        public async Task<IEnumerable<Employee>> ListEmployeesAsync(IQueryable<Employee> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query.AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _unitOfWork.Employees.AddAsync(employee);
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await EmployeeBaseQuery()
                .Where(employee => employee.EmployeeId == employeeId)
                .FirstOrDefaultAsync();
        }

        public void DeleteEmployee(Employee employee)
        {
            _unitOfWork.Remove(employee);
        }

        public async Task<bool> EmployeeExistsAsync(int? employeeId)
        {
            return await EmployeeBaseQuery().AsNoTracking().AnyAsync(employee => employee.EmployeeId == employeeId);
        }

        public async Task<bool> EmailAddressExistsAsync(string email)
        {
            return await EmployeeBaseQuery().AsNoTracking().AnyAsync(employee => employee.Email == email);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
