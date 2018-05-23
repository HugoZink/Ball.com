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
                .Include(p => p.PackageOrders)
                .ThenInclude(po => po.Order)
                .Include(p => p.PackageProducts)
                .ThenInclude(pp => pp.Product)
                .ToListAsync();

            foreach (var package in packages)
            {
                foreach (var po in package.PackageOrders)
                {
                    package.Orders.Add(po.Order);
                }

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
                .Include(p => p.PackageOrders)
                .ThenInclude(po => po.Order)
                .FirstOrDefaultAsync(p => p.PackageId == packageId);

            foreach (var po in package.PackageOrders)
            {
                package.Orders.Add(po.Order);
            }

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

        public async Task UpdatePackageAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePackageAsync(Package package)
        {
            foreach (var po in package.PackageOrders)
            {
                _dbContext.PackageOrders.Attach(po);
                _dbContext.PackageOrders.Remove(po);
            }

            foreach (var pp in package.PackageProducts)
            {
                _dbContext.PackageProducts.Attach(pp);
                _dbContext.PackageProducts.Remove(pp);
            }

            _dbContext.Packages.Attach(package);
            _dbContext.Packages.Remove(package);

            await _dbContext.SaveChangesAsync();
        }
    }
}
