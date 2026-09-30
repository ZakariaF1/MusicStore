using MusicStore.Domain;
using MusicStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task DeleteInvoiceCascadeAsync(Invoice invoice)
        {
            IQueryable<InvoiceItem> invoiceItemsQuery = _invoiceRepository.InvoiceItemBaseQuery();

            invoiceItemsQuery = invoiceItemsQuery.Where(invoiceItem => invoiceItem.InvoiceId == invoice.InvoiceId);

            IEnumerable<InvoiceItem> invoiceItems = await _invoiceRepository.ListInvoiceItemsAsync(invoiceItemsQuery);

            foreach (InvoiceItem invoiceItem in invoiceItems)
            {
                _invoiceRepository.DeleteInvoiceItem(invoiceItem);
            }
            _invoiceRepository.DeleteInvoice(invoice);
        }

        public async Task DeleteInvoiceItemCascadeAsync(InvoiceItem invoiceItem)
        {
            Invoice invoice = await _invoiceRepository.GetInvoiceAsync(invoiceItem.InvoiceId);

            _invoiceRepository.DeleteInvoiceItem(invoiceItem);
            await _invoiceRepository.SaveAsync();

            invoice.Total -= invoiceItem.UnitPrice * invoiceItem.Quantity;

            IQueryable<InvoiceItem> invoiceInvoiceItemsQuery = _invoiceRepository.InvoiceItemBaseQuery();

            invoiceInvoiceItemsQuery = invoiceInvoiceItemsQuery.Where(invoiceInvoiceItem => invoiceInvoiceItem.InvoiceId == invoice.InvoiceId);

            IEnumerable<InvoiceItem> InvoiceInvoiceItems = await _invoiceRepository.ListInvoiceItemsAsync(invoiceInvoiceItemsQuery, true);

            if (InvoiceInvoiceItems.Count() == 0)
            {
                _invoiceRepository.DeleteInvoice(invoice);
            }
        }
    }
}
