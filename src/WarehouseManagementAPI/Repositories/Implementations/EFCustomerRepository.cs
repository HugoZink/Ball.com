using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private WarehouseManagementDbContext _dbContext;

        public EFCustomerRepository(WarehouseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = await _dbContext.Customers.ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            return customer;
        }
    }
}
