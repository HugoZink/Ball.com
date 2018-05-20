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
        public decimal Weight { get; set; }

        [JsonIgnore]
        public List<PackageProduct> PackageProducts { get; set; }

        public Product()
        {
            PackageProducts = new List<PackageProduct>();
        }
    }
}
