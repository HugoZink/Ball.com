using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService
{
    public class EventHandler : IMessageHandlerCallback
    {
        private IMessageHandler _messageHandler;
        
        public EventHandler(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        
        public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
        {
            var messageObject = MessageSerializer.Deserialize(message);
            
//            switch (messageType)
//            {
//                case MessageTypes.CustomerRegistered:
//                    await HandleAsync(messageObject.ToObject<CustomerRegistered>());
//                    break;
//            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true; 
        }
        
        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }
    }
}