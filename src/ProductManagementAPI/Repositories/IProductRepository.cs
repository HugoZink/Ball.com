using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Repositories
{
    public interface IProductRepository
    {
		Task<IEnumerable<Product>> GetAllAsync();

		Task<Product> GetAsync(int id);

		Task<Product> UpdateAsync(Product product);

		Task<Product> CreateAsync(Product product);

	}
}
