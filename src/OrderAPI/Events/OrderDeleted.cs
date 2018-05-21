using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Events
{
    public class OrderDeleted : Event
    {
        public readonly string OrderId;

        public OrderDeleted(Guid messageId, string orderId) :
            base(messageId, MessageTypes.OrderDeleted)
        {
            OrderId = orderId;
        }
    }
}
