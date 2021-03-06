﻿using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderAPI.Events
{
    public class CustomerRegistered : Event
    {
        public readonly string CustomerId;
        public readonly string Name;
        public readonly string Address;
        public readonly string PostalCode;
        public readonly string City;
        public readonly string TelephoneNumber;
        public readonly string EmailAddress;

        public CustomerRegistered(Guid messageId, string customerId, string name, string address, string postalCode, string city,
            string telephoneNumber, string emailAddress) : base(messageId, MessageTypes.CustomerRegistered)
        {
            CustomerId = customerId;
            Name = name;
            Address = address;
            PostalCode = postalCode;
            City = city;
            TelephoneNumber = telephoneNumber;
            EmailAddress = emailAddress;
        }
    }
}
