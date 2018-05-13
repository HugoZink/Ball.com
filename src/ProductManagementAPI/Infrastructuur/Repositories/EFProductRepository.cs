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

		public IEnumerable<Product> Products => _context.Products;

		public async Task<IEnumerable<Product>> GetAllAsync()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetAsync(int id)
		{
			return await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
		}

		public async Task UpdateAsync(Product product)
		{
			var exist = await GetAsync(product.Id);

			if (exist == null)
			{
				throw new KeyNotFoundException();
			}

			_context.Products.Update(product);

			await _context.SaveChangesAsync();
		}

		public async Task CreateAsync(Product product)
		{
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
		}
	}
}
