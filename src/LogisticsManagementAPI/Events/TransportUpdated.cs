using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Events
{
    public class TransportUpdated : Event
    {
        public readonly string TransportId;
        public readonly string CompanyName;
        public readonly string TypeOfShipment;
        public readonly string CountryOfDestination;
        public readonly string Description;
        public readonly decimal WeightInKgMax;
        public readonly decimal ShippingCost;

        public TransportUpdated(Guid messageId, string transportId, string companyName, string typeOfShipment,
            string countryOfDestination, string description, decimal weightInKgMax,
            decimal shippingCost) : base(messageId, MessageTypes.TransportUpdated)
        {
            TransportId = transportId;
            CompanyName = companyName;
            TypeOfShipment = typeOfShipment;
            CountryOfDestination = countryOfDestination;
            Description = description;
            WeightInKgMax = weightInKgMax;
            ShippingCost = shippingCost;
        }
    }
}
