using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.Commands
{
    public class RemoveTransport : Command
    {
        public readonly string TransportId;

        public RemoveTransport(Guid messageId, string transportId) :
            base(messageId, MessageTypes.RemoveTransport)
        {
            TransportId = transportId;
        }
    }
}
