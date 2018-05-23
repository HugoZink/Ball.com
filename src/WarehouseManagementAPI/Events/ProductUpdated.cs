using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagementAPI.Events
{
    public class ProductUpdated : Event
    {
        public readonly string Id;
        public readonly string Name;
        public readonly decimal WeightKg;

        public ProductUpdated(Guid messageId, string id, string name, decimal weightkg) :
            base(messageId, MessageTypes.ProductUpdated)
        {
            Id = id;
            Name = name;
            WeightKg = weightkg;
        }
    }
}
