using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShippingService.Infrastructure.Repositories
{
	public class EFPackageRepository : IPackageRepository
	{
		private ShippingDbContext _context;

		public EFPackageRepository(ShippingDbContext context)
		{
			_context = context;
		}

		public async Task AddPackageAsync(Package package)
		{
			var newPackage = await _context.Package.AddAsync(package);

			await _context.SaveChangesAsync();
		}

		public async Task DeletePackageAsync(Package package)
		{
			var toDeletePackage = _context.Package.FirstOrDefaultAsync(t => t.PackageId == package.PackageId);

			if (toDeletePackage == null)
			{
				throw new KeyNotFoundException();
			}

			_context.Package.Remove(package);

			await _context.SaveChangesAsync();
		}

		public async Task<Package> GetPackageAsync(string packageId)
		{
			return await _context.Package.FirstOrDefaultAsync(t => t.PackageId == packageId);
		}

		public async Task<IEnumerable<Package>> GetPackagesAsync()
		{
			return await _context.Package.ToListAsync();
		}

		public async Task<IEnumerable<Package>> GetPackagesFromYesterdayAsync()
		{

			//var packages = await _context.Package.Where(p => p.TimeOfRecieve.Date == DateTime.Now.Date.AddDays(-1) && p.Shipped !=false).ToListAsync();
			var packages = await _context.Package.Where(p => p.TimeOfRecieve.Date == DateTime.Now.Date.AddDays(-1) && p.Shipped == false).ToListAsync();

			return packages;
		}

		public async Task SetPackageToShippedAsync(string packageId)
		{
			var package = await GetPackageAsync(packageId);

			package.Shipped = true;

			_context.Update(package);

			await _context.SaveChangesAsync();
		}
	}
}
