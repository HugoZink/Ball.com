using Microsoft.EntityFrameworkCore;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Infrastructure.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
		private ShippingDbContext _context;

		public EFOrderRepository(ShippingDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Order>> GetOrdersAsync()
		{
			return await _context.Order.ToListAsync();
		}

		public async Task AddOrderAsync(Order order)
		{
			await _context.Order.AddAsync(order);

			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Order>> GetPackageOrderAsync(string packageId)
		{
			return await _context.Order.Where(o => o.PackageId == packageId).ToListAsync();
		}
	}
}
