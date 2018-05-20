using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class Package
    {
        public string PackageId { get; set; }
        public string TypeOfPackage { get; set; }
        public string Region { get; set; }
        public string ShippingStatus { get; set; }
        public string BarcodeNumber { get; set; }
        public string ZipCode { get; set; }
        public bool Delivered { get; set; }
        public decimal WeightInKgMax { get; set; }
        public DateTime DeliveryTime { get; set; }

        [JsonIgnore]
        public List<PackageProduct> PackageProducts { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }

        public Package()
        {
            PackageProducts = new List<PackageProduct>();
            Products = new List<Product>();
        }
    }
}
