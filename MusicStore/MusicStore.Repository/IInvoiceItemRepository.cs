//using MusicStore.Domain;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace MusicStore.Repository
//{
//    public interface IInvoiceItemRepository
//    {
//        IQueryable<InvoiceItem> InvoiceItemBaseQuery();
//        Task<IEnumerable<InvoiceItem>> ListInvoiceItemsAsync(IQueryable<InvoiceItem> query, bool asNoTracking = false);
//        Task CreateInvoiceItemAsync(InvoiceItem invoiceItem);
//        Task<InvoiceItem> GetInvoiceItemAsync(int invoiceId, int invoiceLineId);
//        void DeleteInvoiceItem(InvoiceItem invoiceItem);
//        Task<bool> InvoiceExistsAsync(int invoiceId);
//        Task SaveAsync();

//    }
//}
