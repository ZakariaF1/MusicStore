using Microsoft.EntityFrameworkCore;
using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository.MySql
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly UnitOfWork _unitOfWork;
        public CustomerRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Customer> CustomerBaseQuery()
        {
            IQueryable<Customer> customersQuery = _unitOfWork.Customers;
            return customersQuery;
        }

        public async Task<IEnumerable<Customer>> ListCustomersAsync(IQueryable<Customer> query, bool asNoTracking)
        {
            if (asNoTracking)
            {
                return await query
                    .AsNoTracking()
                    .Include(p => p.SupportRep)
                    .ToListAsync();
            }
            else
            {
                return await query
                    .Include(p => p.SupportRep)
                    .ToListAsync();
            }
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
        }

        public async Task<Customer> GetCustomerAsync(int customerId)
        {
            return await CustomerBaseQuery()
                .Where(customer => customer.CustomerId == customerId)
                .Include(p => p.SupportRep)
                .FirstOrDefaultAsync();
        }

        public void DeleteCustomer(Customer customer)
        {
            _unitOfWork.Remove(customer);
        }

        public async Task<bool> CustomerExistsAsync(int customerId)
        {
            return await CustomerBaseQuery().AsNoTracking().AnyAsync(customer => customer.CustomerId == customerId);
        }

        public async Task<bool> EmailAddressExistsAsync(string email)
        {
            return await CustomerBaseQuery().AsNoTracking().AnyAsync(customer => customer.Email == email);
        }

        public async Task SaveAsync()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
