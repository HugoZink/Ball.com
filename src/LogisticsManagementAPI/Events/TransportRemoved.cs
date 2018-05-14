using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Events
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
