using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using MusicStore.Domain;

namespace MusicStore.ExtensionMethods
{
    public static class CustomerExtensionMethods
    {
        public static CustomerResponse ToResponseDto(this Customer customer)
        {
            return new CustomerResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Company = customer.Company,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                Country = customer.Country,
                PostalCode = customer.PostalCode,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Email = customer.Email,
                SupportRep = customer.SupportRep.ToResponseDto()
            };
        }

        public static CustomerLiteResponse ToLiteResponseDto(this Customer customer)
        {
            return new CustomerLiteResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Company = customer.Company,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                Country = customer.Country,
                PostalCode = customer.PostalCode,
                Phone = customer.Phone,
                Fax = customer.Fax,
                Email = customer.Email,
                SupportRepId = customer.SupportRepId
            };
        }

        public static Customer ToEntity(this CustomerCreateRequest request)
        {
            return new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Company = request.Company,
                Address = request.Address,
                City = request.City,
                State = request.State,
                Country = request.Country,
                PostalCode = request.PostalCode,
                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email,
                SupportRepId = request.SupportRepId
            };
        }

        public static Customer UpdateCustomer(this Customer customer, CustomerUpdateRequest request)
        {
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Company = request.Company;
            customer.Address = request.Address;
            customer.City = request.City;
            customer.State = request.State;
            customer.Country = request.Country;
            customer.PostalCode = request.PostalCode;
            customer.Phone = request.Phone;
            customer.Fax = request.Fax;
            customer.Email = request.Email;
            customer.SupportRepId = request.SupportRepId;

            return customer;
        }
    }
}
