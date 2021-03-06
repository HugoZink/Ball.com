﻿using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderAPI.Model;

namespace OrderAPI.Events
{
    public class OrderCreated : Event
    {
		public readonly string OrderId;
		public readonly DateTime DateTime;
		public readonly string TrackingCode;
		public readonly List<Product> Products;
		public readonly Customer Customer;

		public OrderCreated(Guid messageId, string orderId, DateTime dateTime,
			string trackingCode, List<Product> products, Customer customer) : base(messageId, MessageTypes.OrderCreated)
        {
			OrderId = orderId;
			DateTime = dateTime;
			TrackingCode = trackingCode;
			Products = products;
			Customer = customer;
		}
	}
}
