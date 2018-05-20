using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Models
{
    public class Transport
    {
        public string TransportId { get; set; }
        public string CompanyName { get; set; }
        public string TypeOfShipment { get; set; }
        public string CountryOfDestination { get; set; }
        public string Description { get; set; }
        public decimal WeightInKgMax { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
