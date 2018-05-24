using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;
using ProductManagementAPI.Infrastructure.Commands;
using ProductManagementAPI.Infrastructure.Events;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace ProductManagementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
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
            var products = await _productRepository.GetAllAsync();

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetAsync(string id)
        {
            var products = _productRepository.GetAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(await products);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AddProduct command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // save new product
            Product product = Mapper.Map<Product>(command);
            var newP = await _productRepository.CreateAsync(product);

            // send event
            NewProductAdded e = Mapper.Map<NewProductAdded>(product);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            // return result
            return CreatedAtRoute("GetById", new { id = newP.Id }, newP);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(string id, [FromBody]UpdateProduct command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toBeUpdatedProduct = _productRepository.GetAsync(id);

            if (toBeUpdatedProduct == null)
            {
                return NotFound();

            }
            else if (command.Id == null)
            {
                command.Id = id;
            }

            Product product = Mapper.Map<Product>(command);
            await _productRepository.UpdateAsync(product);

            // send event
            ProductUpdated e = Mapper.Map<ProductUpdated>(product);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return NoContent();
        }
    }
}
