using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Repositories;

namespace WarehouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        public IOrderRepository _orderRepo;

        public OrdersController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        // GET api/orders
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _orderRepo.GetOrdersAsync());
        }

        // GET api/orders/5
        [HttpGet]
        [Route("{orderId}", Name = "GetOrderById")]
        public async Task<IActionResult> GetAsync(string orderId)
        {
            var order = await _orderRepo.GetOrderAsync(orderId);

            if (order != null)
            {
                return Ok(order);
            }

            return NotFound();
        }
    }
}
