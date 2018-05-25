using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Events
{
    public class NewProductAdded : Event
    {
        public readonly string Id;
        public readonly string Name;
        public readonly decimal Price;
        public readonly decimal WeightKg;

        public NewProductAdded(Guid messageId, string id, string name, decimal price, decimal weightKg) :
            base(messageId, MessageTypes.NewProductAdded)
        {
            Id = id;
            Name = name;
            Price = price;
            WeightKg = weightKg;
        }
    }
}
