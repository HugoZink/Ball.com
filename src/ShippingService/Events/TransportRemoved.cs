using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Events
{
    public class TransportRemoved : Event
    {
        public readonly string TransportId;

        public TransportRemoved(Guid messageId, string transportId) :
            base(messageId, MessageTypes.TransportRemoved)
        {
            TransportId = transportId;
        }
    }
}
