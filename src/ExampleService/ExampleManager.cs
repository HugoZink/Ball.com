using ExampleService.Repositories;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleService
{
    public class ExampleManager : IMessageHandlerCallback
    {
        private IMessagePublisher _messagePublisher;
        private IMessageHandler _messageHandler;
        private IExampleRepository _repo;

        public ExampleManager(IMessagePublisher messagePublisher, IMessageHandler messageHandler, IExampleRepository repo)
        {
            _messagePublisher = messagePublisher;
            _messageHandler = messageHandler;
            _repo = repo;
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
