using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Events
{
    public class TransportUpdated : Event
    {
        public readonly string TransportId;
        public readonly string CompanyName;
        public readonly string TypeOfShipment;
        public readonly string CountryOfDestination;
        public readonly decimal WeightInKgMax;
        public readonly decimal ShippingCost;

        public TransportUpdated(Guid messageId, string transportId, string companyName, string typeOfShipment,
            string countryOfDestination, decimal weightInKgMax,
            decimal shippingCost) : base(messageId, MessageTypes.TransportUpdated)
        {
            TransportId = transportId;
            CompanyName = companyName;
            TypeOfShipment = typeOfShipment;
            CountryOfDestination = countryOfDestination;
            WeightInKgMax = weightInKgMax;
            ShippingCost = shippingCost;
        }
    }
}
