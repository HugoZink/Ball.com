using System.Collections.Generic;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(string id);

        Task<Customer> UpdateAsync(Customer customer);

        Task<Customer> CreateAsync(Customer customer);
    }
}