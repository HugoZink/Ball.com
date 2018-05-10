using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleService
{
    public class ExampleManager : IMessageHandlerCallback
    {
        IMessagePublisher _messagePublisher;
        IMessageHandler _messageHandler;

        public ExampleManager(IMessagePublisher messagePublisher, IMessageHandler messageHandler)
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
