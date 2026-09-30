using MusicStore.Api.Requests.CreateRequests;
using MusicStore.Api.Requests.UpdateRequests;
using MusicStore.Api.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicStore.Api
{
    public interface IInvoiceApiMethods
    {
        Task<ApiResponse<IEnumerable<InvoiceResponse>>> ListInvoicesAsync();
        Task<ApiResponse<InvoiceResponse>> CreateInvoiceAsync(InvoiceFrontendCreateRequest request);
        Task<Tuple<ApiResponse<InvoiceResponse>, ApiResponse<CustomerResponse>>> GetInvoiceFrontendAsync(int invoiceId, int customerId);
        Task<ApiResponse<InvoiceResponse>> DeleteInvoiceAsync(int invoiceId);
        Task<ApiResponse<InvoiceResponse>> UpdateInvoiceAsync(InvoiceFrontendUpdateRequest request);
        Task<ApiResponse<IEnumerable<InvoiceItemResponse>>> ListInvoiceItemsAsync(int invoiceId);
    }
}