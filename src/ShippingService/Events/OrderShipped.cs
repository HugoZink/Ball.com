using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Infrastructure.Events
{
    public class OrderShipped : Event
    {
        private readonly string _trackingCode;

        public OrderShipped(Guid messageId, string trackingCode) : base(messageId, MessageTypes.OrderShipped)
        {
            _trackingCode = trackingCode;
        }
    }
}