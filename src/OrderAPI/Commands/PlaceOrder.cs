using OrderAPI.Model;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAPI.Commands
{
    public class PlaceOrder : Event
    {
		public readonly string OrderId;
		public readonly Customer Customer;
		public readonly DateTime DateTime;
		public readonly string TrackingCode;
		public readonly string State;
		public readonly List<OrderProduct> OrderProducts;
		public readonly bool AfterPayment;

		public PlaceOrder(Guid messageId, string orderId, Customer customer, DateTime dateTime,
			string trackingCode, string state, List<OrderProduct> orderProducts, bool afterPayment) : base(messageId, MessageTypes.PlaceOrder)
		{
			OrderId = orderId;
			Customer = customer;
			DateTime = dateTime;
			TrackingCode = trackingCode;
			State = state;
			OrderProducts = orderProducts;
			AfterPayment = afterPayment;
		}
    }
}
