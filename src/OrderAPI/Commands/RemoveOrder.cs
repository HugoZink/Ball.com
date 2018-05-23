using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Commands
{
    public class RemoveOrder : Command
    {
        public readonly string OrderId;

        public RemoveOrder(Guid messageId, string orderId) :
            base(messageId, MessageTypes.DeleteOrder)
        {
            OrderId = orderId;
        }
    }
}
