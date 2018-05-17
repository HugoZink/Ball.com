using System;
using Pitstop.Infrastructure.Messaging;

namespace ShippingService.Events
{
    public class CustomerRegistrated : Event
    {
        public readonly string Id;
        public readonly string Name;
        public readonly string Adress;
        
        public CustomerRegistrated(Guid messageId, MessageTypes messageType, string name, string adress, string id) : base(messageId, messageType)
        {
            Name = name;
            Adress = adress;
            Id = id;
        }
    }
}