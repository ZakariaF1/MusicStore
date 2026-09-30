using MusicStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Repository
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> CustomerBaseQuery();
        Task<IEnumerable<Customer>> ListCustomersAsync(IQueryable<Customer> query, bool asNoTracking = false);
        Task CreateCustomerAsync(Customer customer);
        Task<Customer> GetCustomerAsync(int customerId);
        void DeleteCustomer(Customer customer);
        Task<bool> CustomerExistsAsync(int customerId);
        Task<bool> EmailAddressExistsAsync(string email);
        Task SaveAsync();
    }
}
