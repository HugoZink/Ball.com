using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class PackageOrder
    {
        public string PackageId { get; set; }
        public Package Package { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
