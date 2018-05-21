using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Events
{
    public class PackageRegistered : Event
    {
        public readonly string PackageId;
        public readonly string TypeOfPackage;
        public readonly string Region;
        public readonly string ShippingStatus;
        public readonly string BarcodeNumber;
        public readonly string ZipCode;
        public readonly bool Delivered;
        public readonly decimal WeightInKgMax;
        public readonly DateTime DeliveryTime;

        public readonly Transport Transport;

        public readonly List<Order> Orders;
        public readonly List<Product> Products;

        public PackageRegistered(Guid messageId, string packageId,
            string typeOfPackage, string region, string shippingStatus,
            string barcodeNumber, string zipCode, bool delivered,
            decimal weightInKgMax, DateTime deliveryTime, Transport transport,
            List<Order> orders, List<Product> products) : base(messageId, MessageTypes.PackageRegistered)
        {
            PackageId = packageId;
            TypeOfPackage = typeOfPackage;
            Region = region;
            ShippingStatus = shippingStatus;
            BarcodeNumber = barcodeNumber;
            ZipCode = zipCode;
            Delivered = delivered;
            WeightInKgMax = weightInKgMax;
            DeliveryTime = deliveryTime;

            Transport = transport;

            Orders = orders;
            Products = products;
        }
    }
}
