using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetPackagesAsync();
        Task<Package> GetPackageAsync(string packageId);
        Task AddPackageAsync(Package package);
    }
}
