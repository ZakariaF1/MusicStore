using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface IInvoiceRepository
    {
        IQueryable<Customer> CustomerBaseQuery();
        IQueryable<Invoice> InvoiceBaseQuery();
        IQueryable<InvoiceItem> InvoiceItemBaseQuery();
        IQueryable<Track> TrackBaseQuery();
        Task<IEnumerable<Invoice>> ListInvoicesAsync(IQueryable<Invoice> query, bool asNoTracking = false);
        Task CreateInvoiceAsync(Invoice invoice);
        Task<Invoice> GetInvoiceAsync(int invoiceId);
        void DeleteInvoice(Invoice invoice);
        Task<IEnumerable<InvoiceItem>> ListInvoiceItemsAsync(IQueryable<InvoiceItem> query, bool asNoTracking = false);
        Task CreateInvoiceItemAsync(InvoiceItem invoiceItem);
        Task<InvoiceItem> GetInvoiceItemAsync(int invoiceId, int invoiceLineId);
        void DeleteInvoiceItem(InvoiceItem invoiceItem);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<bool> InvoiceExistsAsync(int invoiceId);
        Task<bool> TrackExistsAsync(int trackId);
        Task SaveAsync();
    }
}
