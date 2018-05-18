using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShippingDbContext _context;

        public ProductRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(string id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);

            if (product == null)
            {
                throw new KeyNotFoundException();
            }

            return product;
        }
        
        public async Task<Product> UpdateAsync(Product product)
        {

            var updatedP = _context.Products.Update(product);

            var exist = await GetAsync(product.Id);

            await _context.SaveChangesAsync();

            return exist;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var newP = await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return newP.Entity;
        }
    }
}