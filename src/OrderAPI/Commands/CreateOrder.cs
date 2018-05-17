using Pitstop.Infrastructure.Messaging;
using OrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Commands
{
    public class CreateOrder : Command
    {
        public readonly string OrderId;
        public readonly DateTime DateTime;
        public readonly string TrackingCode;
		public readonly List<Product> Products;
		public readonly Customer Customer;

        public CreateOrder(Guid messageId, string orderId, DateTime dateTime,
            string trackingCode, List<Product> products, Customer customer) : base(messageId, MessageTypes.CreateOrder)
        {
            OrderId = orderId;
            DateTime = dateTime;
            TrackingCode = trackingCode;
            Products = products;
            Customer = customer;
        }
    }
}
