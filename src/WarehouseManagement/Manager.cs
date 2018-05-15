using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement
{
    public class Manager : IMessageHandlerCallback
    {
        private IMessageHandler _messageHandler;

        public Manager(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

        public Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
        {
            throw new NotImplementedException();
        }
    }
}
