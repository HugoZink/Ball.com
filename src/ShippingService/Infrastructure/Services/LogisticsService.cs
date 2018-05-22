using ShippingService.Infrastructure.Database;
using ShippingService.Models;
using ShippingService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Infrastructure.Services
{
	class LogisticsService : ILogisticsService
	{
		public String GenerateTrackingCode()
		{
			var trackingCode = Guid.NewGuid().ToString();

			return trackingCode;
		}
	}
}
