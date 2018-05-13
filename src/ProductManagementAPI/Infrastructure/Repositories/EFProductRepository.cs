using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductManagementAPI.Database;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Repositories
{
    public class EFProductRepository : IProductRepository
    {
		private readonly ProductDbContext _context;

		public EFProductRepository(ProductDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.ToListAsync();

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
