using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Repositories;

namespace WarehouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public IProductRepository _productRepo;

        public ProductsController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // GET api/products
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _productRepo.GetProductsAsync());
        }

        // GET api/products/5
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAsync(string productId)
        {
            var product = await _productRepo.GetProductAsync(productId);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }
    }
}
