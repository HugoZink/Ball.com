using System;
using System.Collections.Generic;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Models;

namespace ShippingService.Events
{
    public class OrderShipped : Event
    {
        private readonly string _trackingCode;
        private readonly string _id;
        private  readonly List<Product> _products;

        public OrderShipped(Guid messageId, string trackingCode, string id, List<Product> products) : base(messageId, MessageTypes.OrderShipped)
        {
			_id = id;
			_trackingCode = trackingCode;
            _products = products;
        }
    }
}