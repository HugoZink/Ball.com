using ShippingService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Repositories
{
    public interface IOrderRepository
    {
		Task<IEnumerable<Order>> GetOrdersAsync();
		Task<IEnumerable<Order>> GetPackageOrderAsync(string packageId);
		Task AddOrderAsync(Order order);
	}
}
