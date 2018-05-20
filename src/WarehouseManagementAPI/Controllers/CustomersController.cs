using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagementAPI.Repositories;

namespace WarehouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        public ICustomerRepository _customerRepo;

        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        // GET api/customers
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _customerRepo.GetCustomersAsync());
        }

        // GET api/customers/5
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            var customer = await _customerRepo.GetCustomerAsync(customerId);

            if (customer != null)
            {
                return Ok(customer);
            }

            return NotFound();
        }
    }
}
