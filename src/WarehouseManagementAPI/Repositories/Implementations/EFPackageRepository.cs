using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFPackageRepository : IPackageRepository
    {
        private WarehouseManagementDbContext _dbContext;

        public EFPackageRepository(WarehouseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            var packages = await _dbContext.Packages
                .Include(p => p.PackageProducts)
                .ThenInclude(pp => pp.Product)
                .ToListAsync();

            foreach (var package in packages)
            {
                foreach (var pp in package.PackageProducts)
                {
                    package.Products.Add(pp.Product);
                }
            }

            return packages;
        }

        public async Task<Package> GetPackageAsync(string packageId)
        {
            var package = await _dbContext.Packages
                .Include(p => p.PackageProducts)
                .ThenInclude(pp => pp.Product)
                .FirstOrDefaultAsync(p => p.PackageId == packageId);

            foreach (var pp in package.PackageProducts)
            {
                package.Products.Add(pp.Product);
            }

            return package;
        }

        public async Task AddPackageAsync(Package package)
        {
            _dbContext.Packages.Add(package);

            await _dbContext.SaveChangesAsync();
        }
    }
}
