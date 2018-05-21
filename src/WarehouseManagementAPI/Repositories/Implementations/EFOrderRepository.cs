using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFOrderRepository : IOrderRepository
    {
        private WarehouseManagementDbContext _dbContext;

        public EFOrderRepository(WarehouseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();

            foreach (var order in orders)
            {
                foreach (var op in order.OrderProducts)
                {
                    order.Products.Add(op.Product);
                }
            }

            return orders;
        }

        public async Task<Order> GetOrderAsync(string orderId)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            foreach (var op in order.OrderProducts)
            {
                order.Products.Add(op.Product);
            }

            return order;
        }
    }
}
