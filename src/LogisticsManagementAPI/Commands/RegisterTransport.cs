using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Commands
{
    public class RegisterTransport : Command
    {
        public readonly string TransportId;
        public readonly string CompanyName;
        public readonly string TypeOfShipment;
        public readonly string CountryOfDestination;
        public readonly string Description;
        public readonly decimal WeightInKgMax;
        public readonly decimal ShippingCost;

        public RegisterTransport(Guid messageId, string transportId, string companyName,
            string typeOfShipment, string countryOfDestination, string description,
            decimal weightInKgMax, decimal shippingCost, MessageTypes messageType) : base(messageId, messageType)
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
