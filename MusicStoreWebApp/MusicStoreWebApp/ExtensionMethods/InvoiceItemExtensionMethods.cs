using MusicStore.Api.Responses;
using MusicStoreWebApp.Models.ViewModels;

namespace MusicStoreWebApp.ExtensionMethods
{
    public static class InvoiceItemExtensionMethods
    {
        public static InvoiceItemViewModel ToViewModel(this InvoiceItemResponse invoiceItemResponse)
        {
            return new InvoiceItemViewModel
            {
                InvoiceLineId = invoiceItemResponse.InvoiceLineId,
                InvoiceId = invoiceItemResponse.InvoiceId,
                TrackId = invoiceItemResponse.Track.TrackId,
                TrackName = invoiceItemResponse.Track.Name,
                UnitPrice = invoiceItemResponse.UnitPrice,
                Quantity = invoiceItemResponse.Quantity
            };
        }
    }
}
