using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Database;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;

namespace ProductManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {

		IMessagePublisher _messagePublisher;
		private readonly IProductRepository _productRepository;

		public ProductsController(IProductRepository productRepository, IMessagePublisher messagePublisher)
		{
			_productRepository = productRepository;
			_messagePublisher = messagePublisher;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var products = _productRepository.GetAllAsync();

			return Ok(await products);
		}

		// GET: api/Products/5
		[HttpGet("{id}", Name = "Get")]
		public async Task<IActionResult> Get(int id)
		{
			var products = _productRepository.GetAsync(id);

			if(products == null)
			{
				return NotFound();
			}

			return Ok(await products);
		}
        
        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody]Product product)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var newP = await _productRepository.CreateAsync(product);

			return Ok(newP);
		}
        
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product product)
        {
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var updatedP = await _productRepository.UpdateAsync(product);

			return Ok(updatedP);
		}
    }
}
