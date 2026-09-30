using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public InvoiceRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Customer> CustomerBaseQuery()
        {
            IQueryable<Customer> customersQuery = _unitOfWork.Customers;
            return customersQuery;
        }

        public IQueryable<Invoice> InvoiceBaseQuery()
        {
            IQueryable<Invoice> invoicesQuery = _unitOfWork.Invoices;
            return invoicesQuery;
        }

        public IQueryable<InvoiceItem> InvoiceItemBaseQuery()
        {
            IQueryable<InvoiceItem> invoiceItemsQuery = _unitOfWork.InvoiceItems;
            return invoiceItemsQuery;
        }

        public IQueryable<Track> TrackBaseQuery()
        {
            IQueryable<Track> tracksQuery = _unitOfWork.Tracks;
            return tracksQuery;
        }

        public async Task<IEnumerable<Invoice>> ListInvoicesAsync(IQueryable<Invoice> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.Customer)
                        .ThenInclude(p => p.SupportRep)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.Customer)
                        .ThenInclude(p => p.SupportRep)
                    .ToListAsync();
            }
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            await _unitOfWork.Invoices.AddAsync(invoice);
        }

        public async Task<Invoice> GetInvoiceAsync(int invoiceId)
        {
            return await InvoiceBaseQuery()
                .Where(invoice => invoice.InvoiceId == invoiceId)
                .Include(p => p.Customer)
                    .ThenInclude(p => p.SupportRep)
                .FirstOrDefaultAsync();
        }

        public void DeleteInvoice(Invoice invoice)
        {
            _unitOfWork.Remove(invoice);
        }

        public async Task<IEnumerable<InvoiceItem>> ListInvoiceItemsAsync(IQueryable<InvoiceItem> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.Invoice)
                        .ThenInclude(p => p.Customer)
                            .ThenInclude(p => p.SupportRep)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Album)
                            .ThenInclude(p => p.Artist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Genre)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.MediaType)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.Invoice)
                        .ThenInclude(p => p.Customer)
                            .ThenInclude(p => p.SupportRep)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Album)
                            .ThenInclude(p => p.Artist)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.Genre)
                    .Include(p => p.Track)
                        .ThenInclude(p => p.MediaType)
                    .ToListAsync();
            }
        }

        public async Task CreateInvoiceItemAsync(InvoiceItem invoiceItem)
        {
            Track track = await TrackBaseQuery().Where(p => p.TrackId == invoiceItem.TrackId).FirstOrDefaultAsync();
            Invoice invoice = await InvoiceBaseQuery().Where(p => p.InvoiceId == invoiceItem.InvoiceId).FirstOrDefaultAsync();

            if (invoiceItem.UnitPrice == 0)
            {
                invoiceItem.UnitPrice = track.UnitPrice;
            }
            await _unitOfWork.InvoiceItems.AddAsync(invoiceItem);

            invoice.Total += invoiceItem.UnitPrice * invoiceItem.Quantity;
        }

        public async Task<InvoiceItem> GetInvoiceItemAsync(int invoiceId, int invoiceLineId)
        {
            return await InvoiceItemBaseQuery()
                .Where(invoiceItem => invoiceItem.InvoiceId == invoiceId && invoiceItem.InvoiceLineId == invoiceLineId)
                .Include(p => p.Invoice)
                    .ThenInclude(p => p.Customer)
                        .ThenInclude(p => p.SupportRep)
                .Include(p => p.Track)
                    .ThenInclude(p => p.Album)
                        .ThenInclude(p => p.Artist)
                .Include(p => p.Track)
                    .ThenInclude(p => p.Genre)
                .Include(p => p.Track)
                    .ThenInclude(p => p.MediaType)
                .FirstOrDefaultAsync();
        }

        public void DeleteInvoiceItem(InvoiceItem invoiceItem)
        {
            _unitOfWork.InvoiceItems.Remove(invoiceItem);
        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            return await CustomerBaseQuery().AsNoTracking().AnyAsync(customer => customer.CustomerId == customerId);
        }

        public async Task<bool> InvoiceExistsAsync(int invoiceId)
        {
            return await InvoiceBaseQuery().AsNoTracking().AnyAsync(invoice => invoice.InvoiceId == invoiceId);
        }

        public async Task<bool> TrackExistsAsync(int trackId)
        {
            return await TrackBaseQuery().AsNoTracking().AnyAsync(track => track.TrackId == trackId);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
