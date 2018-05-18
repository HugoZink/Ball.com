using System.Collections.Generic;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Repositories
{
    public interface IOrderRepository
    {
            Task<Order> GetAsync(string id);

            Task<Order> UpdateAsync(Order order);

            Task<Order> CreateAsync(Order order);
    }
}