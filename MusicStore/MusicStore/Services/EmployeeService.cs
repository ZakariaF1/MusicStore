using MusicStore.Domain;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICustomerRepository _customerRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, ICustomerRepository customerRepository)
        {
            _employeeRepository = employeeRepository;
            _customerRepository = customerRepository;
        }

        public async Task DeleteEmployeeCascadeAsync(Employee employee)
        {
            IQueryable<Employee> employeesQuery = _employeeRepository.EmployeeBaseQuery();
            IQueryable<Customer> customersQuery = _customerRepository.CustomerBaseQuery();

            employeesQuery = employeesQuery.Where(p => p.ReportsTo == employee.EmployeeId);
            customersQuery = customersQuery.Where(p => p.SupportRepId == employee.EmployeeId);

            IEnumerable<Employee> employees = await _employeeRepository.ListEmployeesAsync(employeesQuery);
            IEnumerable<Customer> customers = await _customerRepository.ListCustomersAsync(customersQuery);

            foreach (Employee p in employees)
            {
                p.ReportsTo = null;
            }

            foreach (Customer customer in customers)
            {
                customer.SupportRepId = null;
            }
            _employeeRepository.DeleteEmployee(employee);
        }
    }
}
