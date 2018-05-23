using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Events
{
    public class TransportRegistered : Event
    {
        public readonly string TransportId;
        public readonly string CompanyName;
        public readonly string TypeOfShipment;
        public readonly string CityOfDestination;
        public readonly string Description;
        public readonly decimal WeightInKgMax;
        public readonly decimal ShippingCost;

        public TransportRegistered(Guid messageId, string transportId, string companyName,
            string typeOfShipment, string cityOfDestination, string description,
            decimal weightInKgMax, decimal shippingCost) : base(messageId, MessageTypes.TransportRegistered)
        {
            TransportId = transportId;
            CompanyName = companyName;
            TypeOfShipment = typeOfShipment;
            CityOfDestination = cityOfDestination;
            Description = description;
            WeightInKgMax = weightInKgMax;
            ShippingCost = shippingCost;
        }
    }
}
