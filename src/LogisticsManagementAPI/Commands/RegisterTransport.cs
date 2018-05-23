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
        public readonly string CityOfDestination;
        public readonly string Description;
        public readonly decimal WeightInKgMax;
        public readonly decimal ShippingCost;

        public RegisterTransport(Guid messageId, string transportId, string companyName,
            string typeOfShipment, string cityOfDestination, string description,
            decimal weightInKgMax, decimal shippingCost) : base(messageId, MessageTypes.RegisterTransport)
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
