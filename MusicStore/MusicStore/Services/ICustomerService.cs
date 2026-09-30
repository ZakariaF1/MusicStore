using MusicStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Services
{
    public interface ICustomerService
    {
        Task DeleteCustomerCascadeAsync(Customer customer);
    }
}
