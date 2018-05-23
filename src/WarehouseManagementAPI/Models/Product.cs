using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public int Amount { get; set; }

        [JsonIgnore]
        public List<PackageProduct> PackageProducts { get; set; }

        [JsonIgnore]
        public List<OrderProduct> OrderProducts { get; set; }

        public Product()
        {
            PackageProducts = new List<PackageProduct>();
            OrderProducts = new List<OrderProduct>();
        }
    }
}
