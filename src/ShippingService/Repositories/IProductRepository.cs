using System.Collections.Generic;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(string id);

        Task<Product> UpdateAsync(Product product);

        Task<Product> CreateAsync(Product product);
    }
}