using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.ExtensionMethods
{
    public static class EmployeeExtensionMethods
    {
        public static EmployeeResponse ToResponseDto(this Employee employee)
        {
            if (employee == null)
            {
                return null;
            }
            else
            {
                return new EmployeeResponse
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Title = employee.Title,
                    ReportsTo = employee.ReportsTo,
                    BirthDate = employee.BirthDate,
                    HireDate = employee.HireDate,
                    Address = employee.Address,
                    City = employee.City,
                    State = employee.State,
                    Country = employee.Country,
                    PostalCode = employee.PostalCode,
                    Phone = employee.Phone,
                    Fax = employee.Fax,
                    Email = employee.Email
                };
            }
        }

        public static Employee ToEntity(this EmployeeCreateRequest request)
        {
            return new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Title = request.Title,
                ReportsTo = request.ReportsTo,
                BirthDate = request.BirthDate,
                HireDate = request.HireDate,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email
            };
        }

        public static Employee UpdateEmployee(this Employee employee, EmployeeUpdateRequest request)
        {
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Title = request.Title;
            employee.ReportsTo = request.ReportsTo;
            employee.BirthDate = request.BirthDate;
            employee.HireDate = request.HireDate;
            employee.Address = request.Address;
            employee.City = request.City;
            employee.State = request.State;
            employee.Country = request.Country;
            employee.PostalCode = request.PostalCode;
            employee.Phone = request.Phone;
            employee.Fax = request.Fax;
            employee.Email = request.Email;

            return employee;
        }
    }
}
