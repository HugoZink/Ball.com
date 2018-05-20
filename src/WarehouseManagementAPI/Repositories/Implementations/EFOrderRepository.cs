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
            var orders = await _dbContext.Orders.ToListAsync();

            return orders;
        }

        public async Task<Order> GetOrderAsync(string orderId)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(t => t.OrderId == orderId);

            return order;
        }
    }
}
