using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Events;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI
{
    public class Manager : IMessageHandlerCallback
    {
        private IMessageHandler _messageHandler;
        private string _sqlConnectionString;

        public Manager(IMessageHandler messageHandler, string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
            _messageHandler = messageHandler;
        }

        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

        private WarehouseManagementDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WarehouseManagementDbContext>();
            optionsBuilder.UseSqlServer(_sqlConnectionString);

            return new WarehouseManagementDbContext(optionsBuilder.Options);
        }

        public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
        {
            JObject messageObject = MessageSerializer.Deserialize(message);

            if (messageObject != null)
            {
                switch (messageType)
                {
                    // Events registered to the database
                    case MessageTypes.CustomerRegistered:
                        await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                        break;
                    case MessageTypes.NewProductAdded:
                        await HandleAsync(messageObject.ToObject<NewProductAdded>());
                        break;
                    case MessageTypes.TransportRegistered:
                        await HandleAsync(messageObject.ToObject<TransportRegistered>());
                        break;
                    case MessageTypes.OrderPlaced:
                        await HandleAsync(messageObject.ToObject<OrderPlaced>());
                        break;

                    // Events updated to the database
                    case MessageTypes.ProductUpdated:
                        await HandleAsync(messageObject.ToObject<ProductUpdated>());
                        break;
                    case MessageTypes.TransportUpdated:
                        await HandleAsync(messageObject.ToObject<TransportUpdated>());
                        break;

                    // Events deleted to the database
                    case MessageTypes.TransportRemoved:
                        await HandleAsync(messageObject.ToObject<TransportRemoved>());
                        break;
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(CustomerRegistered e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    await dbContext.Customers.AddAsync(new Customer
                    {
                        CustomerId = e.CustomerId,
                        Name = e.Name,
                        Address = e.Address,
                        PostalCode = e.PostalCode,
                        City = e.City,
                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(NewProductAdded e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    await dbContext.Products.AddAsync(new Product
                    {
                        ProductId = e.Id,
                        Name = e.Name,
                        Weight = e.WeightKg,
                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(ProductUpdated e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    var product = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == e.Id);
                    product.Name = e.Name;
                    product.Weight = e.WeightKg;

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(TransportRegistered e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    await dbContext.Transports.AddAsync(new Transport
                    {
                        TransportId = e.TransportId,
                        CompanyName = e.CompanyName,
                        TypeOfShipment = e.TypeOfShipment,
                        CityOfDestination = e.CityOfDestination,
                        Description = e.Description,
                        WeightInKgMax = e.WeightInKgMax,
                        ShippingCost = e.ShippingCost,
                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(TransportUpdated e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    var transport = await dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == e.TransportId);
                    transport.CompanyName = e.CompanyName;
                    transport.TypeOfShipment = e.TypeOfShipment;
                    transport.CityOfDestination = e.CityOfDestination;
                    transport.Description = e.Description;
                    transport.WeightInKgMax = e.WeightInKgMax;
                    transport.ShippingCost = e.ShippingCost;

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(TransportRemoved e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    var transport = await dbContext.Transports.FirstOrDefaultAsync(t => t.TransportId == e.TransportId);
                    dbContext.Transports.Attach(transport);
                    dbContext.Transports.Remove(transport);

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(OrderPlaced e)
        {
            using (var dbContext = GetDbContext())
            {
                if (e != null)
                {
                    await dbContext.Orders.AddAsync(new Order
                    {
                        OrderId = e.OrderId,
                        Customer = e.Customer,
                        Destination = e.Customer.City,
                        DateTime = e.DateTime,
                        OrderProducts = e.OrderProducts,

                    });

                    await dbContext.SaveChangesAsync();
                }
            }

            return true;
        }
    }
}
