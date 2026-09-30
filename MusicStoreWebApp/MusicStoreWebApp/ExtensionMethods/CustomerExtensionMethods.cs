using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class CustomerExtensionMethods
    {
        public static CustomerUpdateRequest ToUpdateRequestDto(this CustomerResponse customerResponse)
        {
            return new CustomerUpdateRequest
            {
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName,
                Company = customerResponse.Company,
                Address = customerResponse.Address,
                City = customerResponse.City,
                State = customerResponse.State,
                Country = customerResponse.Country,
                PostalCode = customerResponse.PostalCode,
                Phone = customerResponse.Phone,
                Fax = customerResponse.Fax,
                Email = customerResponse.Email,
                SupportRepId = customerResponse.SupportRep?.EmployeeId
            };
        }

        public static CustomerViewModel ToViewModel(this CustomerResponse customerResponse)
        {
            return new CustomerViewModel
            {
                CustomerId = customerResponse.CustomerId,
                FirstName = customerResponse.FirstName,
                LastName = customerResponse.LastName,
                Company = customerResponse.Company,
                Address = customerResponse.Address,
                City = customerResponse.City,
                State = customerResponse.State,
                Country = customerResponse.Country,
                PostalCode = customerResponse.PostalCode,
                Phone = customerResponse.Phone,
                Fax = customerResponse.Fax,
                Email = customerResponse.Email,
                SupportRep = customerResponse.SupportRep?.FullName
            };
        }
    }
}
