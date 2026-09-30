using System;
using System.Linq;
using MusicStore.Domain;

namespace MusicStore.Repository.MsSql
{
    public class EmployeeRepository
    {

        public IQueryable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(Employee employee)
        {
            //implement of mssql specific implementation
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
