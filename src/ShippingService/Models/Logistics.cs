namespace ShippingService.Models
{
    public class Logistics
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeOfShipment { get; set; }
        public string CountryOfDestination { get; set; }
        public decimal WeightInKgMax { get; set; }
        public decimal ShippingCost { get; set; }
    }
}