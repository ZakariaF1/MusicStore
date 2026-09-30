using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;
using System;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class EmployeeExtensionMethods
    {
        public static string ToString(this DateTime? dt, string format)
    => dt == null ? null : ((DateTime)dt).ToString(format);

        public static EmployeeUpdateRequest ToUpdateRequestDto(this EmployeeResponse employeeResponse)
        {
            return new EmployeeUpdateRequest
            {
                FirstName = employeeResponse.FirstName,
                LastName = employeeResponse.LastName,
                Title = employeeResponse.Title,
                ReportsTo = employeeResponse.ReportsTo,
                BirthDate = employeeResponse.BirthDate,
                HireDate = employeeResponse.HireDate,
                Address = employeeResponse.Address,
                City = employeeResponse.City,
                State = employeeResponse.State,
                Country = employeeResponse.Country,
                PostalCode = employeeResponse.PostalCode,
                Phone = employeeResponse.Phone,
                Fax = employeeResponse.Fax,
                Email = employeeResponse.Email
            };
        }

        public static EmployeeViewModel ToViewModel(this EmployeeResponse employeeResponse, string reportsTo = "")
        {
            Nullable<DateTime> employeeBirthDate = employeeResponse.BirthDate;
            String stringBirthDate = employeeBirthDate.ToString("dd/MM/yyyy");

            Nullable<DateTime> employeeHireDate = employeeResponse.HireDate;
            String stringHireDate = employeeHireDate.ToString("dd/MM/yyyy");

            return new EmployeeViewModel
            {
                EmployeeId = employeeResponse.EmployeeId,
                FirstName = employeeResponse.FirstName,
                LastName = employeeResponse.LastName,
                Title = employeeResponse.Title,
                ReportsTo = reportsTo,
                BirthDate = stringBirthDate,
                HireDate = stringHireDate,
                Address = employeeResponse.Address,
                City = employeeResponse.City,
                State = employeeResponse.State,
                Country = employeeResponse.Country,
                PostalCode = employeeResponse.PostalCode,
                Phone = employeeResponse.Phone,
                Fax = employeeResponse.Fax,
                Email = employeeResponse.Email
            };
        }
    }
}
