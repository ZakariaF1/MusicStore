using MusicStore.Api.Requests.UpdateRequests;
using MusicStoreWebApp.Models.ViewModels;
using MusicStore.Api.Responses;
using System;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class InvoiceExtensionMethods
    {
        public static InvoiceUpdateRequest ToUpdateRequestDto(this InvoiceResponse invoiceResponse)
        {
            return new InvoiceUpdateRequest
            {
                InvoiceDate = invoiceResponse.InvoiceDate,
                CustomerId = invoiceResponse.Customer.CustomerId,
                BillingAddress = invoiceResponse.BillingAddress,
                BillingCity = invoiceResponse.BillingCity,
                BillingState = invoiceResponse.BillingState,
                BillingCountry = invoiceResponse.BillingCountry,
                BillingPostalCode = invoiceResponse.BillingPostalCode
            };
        }

        public static InvoiceViewModel ToViewModel(this InvoiceResponse invoiceResponse)
        {
            Nullable<DateTime> invoiceDate = invoiceResponse.InvoiceDate;
            String stringinvoiceDate = invoiceDate.ToString("dd/MM/yyyy");

            return new InvoiceViewModel
            {
                InvoiceId = invoiceResponse.InvoiceId,
                CustomerId = invoiceResponse.Customer.CustomerId.ToString(),
                CustomerName = invoiceResponse.Customer.FullName,
                InvoiceDate = stringinvoiceDate,
                BillingAddress = invoiceResponse.BillingAddress,
                BillingCity = invoiceResponse.BillingCity,
                BillingState = invoiceResponse.BillingState,
                BillingCountry = invoiceResponse.BillingCountry,
                BillingPostalCode = invoiceResponse.BillingPostalCode,
                Total = invoiceResponse.Total.ToString()
            };
        }
    }
}
