using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;
using OrderAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using OrderAPI.Model;
using OrderAPI.Events;
using AutoMapper;
using OrderAPI.Commands;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
		IMessagePublisher _messagePublisher;
		OrderDbContext _dbContext;

		public OrdersController(OrderDbContext dbContext, IMessagePublisher messagePublisher)
		{
			_dbContext = dbContext;
			_messagePublisher = messagePublisher;
		}

		[HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
			return Ok(await _dbContext.Orders.ToListAsync());
		}

		[HttpGet]
		[Route("{orderId}", Name = "GetByOrderId")]
		public async Task<IActionResult> GetByOrderId(string orderId)
        {
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
			if(order == null)
			{
				return NotFound();
			}

			return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
			var order = new Order();
			order.AddStateChange(OrderState.PENDING);

			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();

			return CreatedAtRoute("GetByOrderId", new { orderId = order.OrderId }, order);
        }

		[HttpPost]
		[Route("{orderId}", Name = "AddProductToOrder")]
		public async Task<IActionResult> AddProductToOrder(string orderId, [FromBody] string productId, [FromBody] int quantity)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
			if (order == null)
			{
				return NotFound();
			}

			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
			if (product == null)
			{
				return NotFound();
			}

			//Create link model
			var orderProduct = new OrderProduct();
			orderProduct.Order = order;
			orderProduct.Product = product;
			orderProduct.Quantity = quantity;

			order.OrderProducts.Add(orderProduct);
			await _dbContext.SaveChangesAsync();

			return AcceptedAtRoute("GetByOrderId", new { orderId = order.OrderId }, order);
		}

		[HttpPost]
		[Route("{orderId}/place", Name = "PlaceOrder")]
		public async Task<IActionResult> PlaceOrder(string orderId, [FromBody] PlaceOrder orderCommand)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
			if (order == null)
			{
				return NotFound();
			}

			order.AfterPayment = orderCommand.AfterPayment;

			if(order.AfterPayment)
			{
				order.AddStateChange(OrderState.AWAITINGAFTERPAYMENT);
			}
			else
			{
				order.AddStateChange(OrderState.PAYMENTINPROGRESS);
			}

			await _dbContext.SaveChangesAsync();

			OrderPlaced e = Mapper.Map<OrderPlaced>(order);
			await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

			return AcceptedAtRoute("GetByOrderId", new { orderId = order.OrderId }, order);
		}

		[HttpDelete]
		[Route("{orderId}/{productId}", Name = "RemoveProductFromOrder")]
		public async Task<IActionResult> RemoveProductFromOrder(string orderId, string productId)
		{
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
			if (order == null)
			{
				return NotFound("Could not find order");
			}

			var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
			if (product == null)
			{
				return NotFound("Could not find product");
			}

			//Find link model
			var orderProduct = order.OrderProducts.FirstOrDefault(op => op.OrderId == order.OrderId && op.ProductId == op.ProductId);
			if(orderProduct == null)
			{
				return NotFound("This product has not been added to this order.");
			}

			order.OrderProducts.Remove(orderProduct);

			await _dbContext.SaveChangesAsync();

			return AcceptedAtRoute("GetByOrderId", new { orderId = order.OrderId }, order);
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string orderId)
        {
			var order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
			if (order == null)
			{
				return NotFound();
			}

			if(order.CurrentState != OrderState.PENDING)
			{
				return BadRequest("Cannot delete a non-pending order.");
			}

			_dbContext.Orders.Remove(order);

			await _dbContext.SaveChangesAsync();

			return Accepted();
		}
    }
}
