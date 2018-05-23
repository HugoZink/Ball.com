using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Models
{
    public class Transport
    {
        public string TransportId { get; set; }
        public string CompanyName { get; set; }
        public string TypeOfShipment { get; set; }
        public string CityOfDestination { get; set; }
        public string Description { get; set; }
        public decimal WeightInKgMax { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
