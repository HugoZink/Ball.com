using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Events
{
    public class OrderPlaced : Event
    {
        public readonly string OrderId;
        public readonly Customer Customer;
        public readonly DateTime DateTime;
        public readonly List<OrderProduct> OrderProducts;

        public OrderPlaced(Guid messageId, string orderId, Customer customer, DateTime dateTime,
            List<OrderProduct> orderProducts) : base(messageId, MessageTypes.OrderPlaced)
        {
            OrderId = orderId;
            Customer = customer;
            DateTime = dateTime;
            OrderProducts = orderProducts;
        }
    }
}
