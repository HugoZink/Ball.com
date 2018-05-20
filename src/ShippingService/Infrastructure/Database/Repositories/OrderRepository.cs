using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShippingDbContext _context;
        

        public OrderRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetAsync(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(s => s.OrderId == id);

            if (order == null)
            {
                throw new KeyNotFoundException();
            }

            return order;
        }
        
        public async Task<Order> UpdateAsync(Order order)
        {

            var updatedP = _context.Orders.Update(order);

            var exist = await GetAsync(order.OrderId);

            await _context.SaveChangesAsync();

            return exist;
        }

        public async Task<Order> CreateAsync(Order orders)
        {
            var newP = await _context.Orders.AddAsync(orders);

            await _context.SaveChangesAsync();

            return newP.Entity;
        }

		public async Task<String> AddTrackingCodeAsync(string id, string trackingCode)
		{
			var order = await GetAsync(id);

			order.TrackingCode = trackingCode;

			await UpdateAsync(order);

			await _context.SaveChangesAsync();

			return trackingCode;
		}
	}
}