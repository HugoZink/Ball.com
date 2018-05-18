using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShippingDbContext _context;

        public CustomerRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetAsync(string id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Id == id);

            if (customer == null)
            {
                throw new KeyNotFoundException();
            }

            return customer;
        }
        
        public async Task<Customer> UpdateAsync(Customer customer)
        {

            var updatedP = _context.Customers.Update(customer);

            var exist = await GetAsync(customer.Id);

            await _context.SaveChangesAsync();

            return exist;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            var newP = await _context.Customers.AddAsync(customer);

            await _context.SaveChangesAsync();

            return newP.Entity;
        }
    }
}