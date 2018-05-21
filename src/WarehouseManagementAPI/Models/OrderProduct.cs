using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class OrderProduct
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
