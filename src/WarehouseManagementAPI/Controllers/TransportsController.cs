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
        public ITransportRepository _transportRepo;

        public TransportsController(ITransportRepository transportRepo)
        {
            _transportRepo = transportRepo;
        }

        // GET api/transports
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _transportRepo.GetTransportsAsync());
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
