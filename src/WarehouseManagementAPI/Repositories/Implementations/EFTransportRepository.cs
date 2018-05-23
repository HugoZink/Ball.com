using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFTransportRepository : ITransportRepository
    {
        private WarehouseManagementDbContext _dbContext;

        public EFTransportRepository(WarehouseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transport>> GetTransportsAsync()
        {
            var transports = await _dbContext.Transports.ToListAsync();

            return transports;
        }

        public async Task<IEnumerable<Transport>> GetTransportsAsync(string packageId)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.PackageId == packageId);

            var transports = await _dbContext.Transports
                .Where(t => package.TypeOfPackage == t.Description)
                .Where(t => package.Region == t.CityOfDestination)
                .Where(t => package.WeightInKgMax <= t.WeightInKgMax)
                .OrderBy(t => t.ShippingCost)
                .ToListAsync();

            return transports;
        }

        public async Task<Transport> GetTransportAsync(string transportId)
        {
            var transport = await _dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == transportId);

            return transport;
        }
    }
}
