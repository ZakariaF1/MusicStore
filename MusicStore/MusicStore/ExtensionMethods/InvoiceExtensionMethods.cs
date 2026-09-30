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
    public static class InvoiceExtensionMethods
    {
        public static InvoiceResponse ToResponseDto(this Invoice invoice)
        {
            return new InvoiceResponse
            {
                InvoiceId = invoice.InvoiceId,
                Customer = invoice.Customer.ToResponseDto(),
                InvoiceDate = invoice.InvoiceDate,
                BillingAddress = invoice.BillingAddress,
                BillingCity = invoice.BillingCity,
                BillingState = invoice.BillingState,
                BillingCountry = invoice.BillingCountry,
                BillingPostalCode = invoice.BillingPostalCode,
                Total = invoice.Total
            };
        }

        public static InvoiceLiteResponse ToLiteResponseDto(this Invoice invoice)
        {
            return new InvoiceLiteResponse
            {
                InvoiceId = invoice.InvoiceId,
                CustomerId = invoice.CustomerId,
                InvoiceDate = invoice.InvoiceDate,
                BillingAddress = invoice.BillingAddress,
                BillingCity = invoice.BillingCity,
                BillingState = invoice.BillingState,
                BillingCountry = invoice.BillingCountry,
                BillingPostalCode = invoice.BillingPostalCode,
                Total = invoice.Total
            };
        }

        public static Invoice ToEntity(this InvoiceCreateRequest request)
        {
            return new Invoice
            {
                InvoiceDate = request.InvoiceDate,
                CustomerId = request.CustomerId,
                BillingAddress = request.BillingAddress,
                BillingCity = request.BillingCity,
                BillingState = request.BillingState,
                BillingCountry = request.BillingCountry,
                BillingPostalCode = request.BillingPostalCode
            };
        }

        public static Invoice UpdateInvoice(this Invoice invoice, InvoiceUpdateRequest request)
        {
            invoice.CustomerId = request.CustomerId;
            invoice.InvoiceDate = request.InvoiceDate;
            invoice.BillingAddress = request.BillingAddress;
            invoice.BillingCity = request.BillingCity;
            invoice.BillingState = request.BillingState;
            invoice.BillingCountry = request.BillingCountry;
            invoice.BillingPostalCode = request.BillingPostalCode;

            return invoice;
        }
    }
}
