using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Events;
using Pitstop.Infrastructure.Messaging;

namespace PaymentAPI.Controllers
{
	[Route("api/[controller]")]
	public class PaymentController : Controller
    {
		IMessagePublisher _messagePublisher;

		public PaymentController(IMessagePublisher messagePublisher)
		{
			_messagePublisher = messagePublisher;
		}

		// POST api/payment
		[HttpPost]
		public async Task<IActionResult> PostAsync(string orderId, [FromBody]string bank)
		{

			OrderPayed e = new OrderPayed(Guid.NewGuid(), orderId, bank);

			await _messagePublisher.PublishMessageAsync(MessageTypes.OrderPayed, e, "");

			return Ok();
		}
	}
}