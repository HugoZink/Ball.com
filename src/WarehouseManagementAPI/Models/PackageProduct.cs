using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class PackageProduct
    {
        public string PackageId { get; set; }
        public Package Package { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
