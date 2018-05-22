using ShippingService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShippingService.Services
{
    public interface ILogisticsService
    {
		String GenerateTrackingCode();
	}
}
