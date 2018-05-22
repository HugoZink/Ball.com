using ShippingService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShippingService.Repositories
{
	public interface IPackageRepository
    {
		Task<IEnumerable<Package>> GetPackagesAsync();
		Task<Package> GetPackageAsync(string packageId);
		Task AddPackageAsync(Package package);
		Task<IEnumerable<Package>> GetPackagesFromYesterdayAsync();
		Task DeletePackageAsync(Package package);
		Task SetPackageToShippedAsync(string packageId);
	}
}
