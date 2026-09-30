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
    public static class InvoiceItemExtensionMethods
    {
        public static InvoiceItemResponse ToResponseDto(this InvoiceItem invoiceItem)
        {
            return new InvoiceItemResponse
            {
                InvoiceLineId = invoiceItem.InvoiceLineId,
                InvoiceId = invoiceItem.InvoiceId,
                Track = invoiceItem.Track.ToResponseDto(),
                UnitPrice = invoiceItem.UnitPrice,
                Quantity = invoiceItem.Quantity
            };
        }

        public static InvoiceItemLiteResponse ToLiteResponseDto(this InvoiceItem invoiceItem)
        {
            return new InvoiceItemLiteResponse
            {
                InvoiceLineId = invoiceItem.InvoiceLineId,
                InvoiceId = invoiceItem.InvoiceId,
                TrackId = invoiceItem.TrackId,
                UnitPrice = invoiceItem.UnitPrice,
                Quantity = invoiceItem.Quantity
            };
        }

        public static InvoiceItem ToEntity(this InvoiceItemCreateRequest request)
        {
            return new InvoiceItem
            {
                TrackId = request.TrackId,
                UnitPrice = request.UnitPrice,
                Quantity = request.Quantity
            };
        }

        public static InvoiceItem UpdateInvoiceItem(this InvoiceItem invoiceItem, InvoiceItemUpdateRequest request)
        {

            invoiceItem.TrackId = request.TrackId;
            invoiceItem.UnitPrice = request.UnitPrice;
            invoiceItem.Quantity = request.Quantity;

            return invoiceItem;
        }
    }
}
