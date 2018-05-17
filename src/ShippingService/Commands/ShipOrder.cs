using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Commands
{
    public class ShipOrder : Command
    {
        public ShipOrder(Guid messageId, MessageTypes messageType) : base(messageId, messageType)
        {
        }
    }
}