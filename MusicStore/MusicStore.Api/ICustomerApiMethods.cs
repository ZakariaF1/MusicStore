using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface ICustomerApiMethods
    {
        Task<ApiResponse<IEnumerable<CustomerResponse>>> ListCustomersAsync();
        Task<ApiResponse<CustomerResponse>> CreateCustomerAsync(CustomerCreateRequest request);
        Task<ApiResponse<CustomerResponse>> GetCustomerAsync(int customerId);
        Task<ApiResponse<CustomerFrontendResponse>> GetCustomerFrontendAsync(int customerId);
        Task<ApiResponse<CustomerResponse>> DeleteCustomerAsync(int customerId);
        Task<ApiResponse<CustomerResponse>> UpdateCustomerAsync(int customerId, CustomerUpdateRequest request);
        Task<ApiResponse<string>> ValidateEmailAddress(int customerId, string email);

    }
}