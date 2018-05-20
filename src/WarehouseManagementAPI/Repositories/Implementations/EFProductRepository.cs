using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFProductRepository : IProductRepository
    {
        private WarehouseManagementDbContext _dbContext;

        public EFProductRepository(WarehouseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _dbContext.Products.ToListAsync();

            return products;
        }

        public async Task<Product> GetProductAsync(string productId)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

            return product;
        }
    }
}
