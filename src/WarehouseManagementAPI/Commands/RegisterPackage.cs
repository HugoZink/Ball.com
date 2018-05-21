using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Commands
{
    public class RegisterPackage : Command
    {
        public string PackageId { get; set; }
        public string TypeOfPackage { get; set; }
        public string Region { get; set; }
        public string ShippingStatus { get; set; }
        public string BarcodeNumber { get; set; }
        public string ZipCode { get; set; }
        public bool Delivered { get; set; }
        public decimal WeightInKgMax { get; set; }
        public DateTime DeliveryTime { get; set; }

        public Transport Transport { get; set; }

        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }

        public RegisterPackage(Guid messageId, string packageId,
            string typeOfPackage, string region, string shippingStatus,
            string barcodeNumber, string zipCode, bool delivered,
            decimal weightInKgMax, DateTime deliveryTime, Transport transport,
            List<Order> orders, List<Product> products) : base(messageId, MessageTypes.RegisterPackage)
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
