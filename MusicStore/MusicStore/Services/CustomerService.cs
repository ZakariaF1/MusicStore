using MusicStore.Domain;
using MusicStore.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public CustomerService(ICustomerRepository customerRepository, IInvoiceRepository invoiceRepository)
        {
            _customerRepository = customerRepository;
            _invoiceRepository = invoiceRepository;

        }

        public async Task DeleteCustomerCascadeAsync(Customer customer)
        {
            IQueryable<Invoice> invoicesQuery = _invoiceRepository.InvoiceBaseQuery();

            invoicesQuery = invoicesQuery.Where(invoice => invoice.CustomerId == customer.CustomerId);

            IEnumerable<Invoice> invoices = await _invoiceRepository.ListInvoicesAsync(invoicesQuery);

            foreach (Invoice invoice in invoices)
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
            _customerRepository.DeleteCustomer(customer);
        }
    }
}
