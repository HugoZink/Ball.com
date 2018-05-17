using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService.Infrastructure.Repositories
{
    public class LogisticsRepository : ILogisticsRepository
    {
        private readonly ShippingDbContext _context;
        

        public LogisticsRepository(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Logistics>> GetAllAsync() => await _context.Logistics.ToListAsync();

        public async Task<Logistics> GetAsync(string id)
        {
            var logistics = await _context.Logistics.FirstOrDefaultAsync(s => s.Id == id);

            if (logistics == null)
            {
                throw new KeyNotFoundException();
            }

            return logistics;
        }
        
        public async Task<Logistics> UpdateAsync(Logistics logistics)
        {

            var updatedP = _context.Logistics.Update(logistics);

            var exist = await GetAsync(logistics.Id);

            await _context.SaveChangesAsync();

            return exist;
        }

        public async Task<Logistics> CreateAsync(Logistics logistics)
        {
            var newP = await _context.Logistics.AddAsync(logistics);

            await _context.SaveChangesAsync();

            return newP.Entity;
        }

        public async Task RemoveAsync(string id)
        {
            var transport = GetAsync(id);
            
            // Delete Transport
            _context.Logistics.Attach(await transport);
            _context.Logistics.Remove(await transport);
            
            await _context.SaveChangesAsync();
        }
    }
}