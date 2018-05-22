using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;

namespace PaymentAPI.Controllers
{
	[Route("api/[controller]")]
	public class PaymentController : Controller
    {
		// POST api/payment
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody]string bank)
		{
			//await IMessagePublisher.PublishMessageAsync(e.MessageType, e, "");

			return Ok();
		}
	}
}