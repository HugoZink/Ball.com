using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Destination { get; set; }
        public DateTime DateTime { get; set; }

        public Customer Customer { get; set; }

        [JsonIgnore]
        public List<PackageOrder> PackageOrders { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }

        public Order()
        {
            PackageOrders = new List<PackageOrder>();
            OrderProducts = new List<OrderProduct>();
            Products = new List<Product>();
        }
    }
}
