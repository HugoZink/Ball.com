using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Repositories;

namespace WarehouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class TransportsController : Controller
    {
        public IPackageRepository _packageRepo;
        public ITransportRepository _transportRepo;

        public TransportsController(IPackageRepository packageRepo, ITransportRepository transportRepo)
        {
            _transportRepo = transportRepo;
            _packageRepo = packageRepo;
        }

        // GET api/transports
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _transportRepo.GetTransportsAsync());
        }

        // GET api/transports/packages/5
        [HttpGet]
        [Route("packages/{packageId}", Name = "GetTransportsByPackageId")]
        public async Task<IActionResult> GetTransportsByPackageIdAsync(string packageId)
        {
            var package = await _packageRepo.GetPackageAsync(packageId);

            if (package != null)
            {
                return Ok(await _transportRepo.GetTransportsAsync(package.PackageId));
            }

            return NotFound();
        }

        // GET api/transports/5
        [HttpGet("{transportId}")]
        public async Task<IActionResult> GetAsync(string transportId)
        {
            var transport = await _transportRepo.GetTransportAsync(transportId);

            if (transport != null)
            {
                return Ok(transport);
            }

            return NotFound();
        }
    }
}
