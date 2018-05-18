using System.Collections.Generic;
using System.Threading.Tasks;
using ShippingService.Models;

namespace ShippingService.Repositories
{
    public interface ILogisticsRepository
    {
        Task<Logistics> GetAsync(string id);

        Task<Logistics> UpdateAsync(Logistics logistics);

        Task<Logistics> CreateAsync(Logistics logistics);
        
        Task RemoveAsync(string id);
    }
}