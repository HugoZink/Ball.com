using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using OrderAPI.DataAccess;
using OrderAPI.Events;
using OrderAPI.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAPI.EventHandler
{
    public class EventHandler : IMessageHandlerCallback
    {
        private string _connectionString;
        private IMessageHandler _messageHandler;

        public EventHandler(IMessageHandler messageHandler, string connectionString)
        {
            _connectionString = connectionString;
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

        public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
        {
            JObject messageObject = MessageSerializer.Deserialize(message);
            try
            {
                switch (messageType)
                {
                    case MessageTypes.CustomerRegistered:
                        await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                        break;
                    case MessageTypes.NewProductAdded:
                        await HandleAsync(messageObject.ToObject<NewProductAdded>());
                        break;
                    case MessageTypes.OrderPayed:
                        await HandleAsync(messageObject.ToObject<OrderPayed>());
                        break;
                }
            }
            catch (Exception ex)
            {
                string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
                Console.WriteLine($"Error while handling {messageType} message with id {messageId}.\n{ex.ToString()}");
            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true;
        }

        private async Task<bool> HandleAsync(CustomerRegistered e)
        {
            Console.WriteLine($"Customer registered: Customer Id = {e.CustomerId}, Name = {e.Name}, Telephone Number = {e.TelephoneNumber}");

            using (var db = GetOrderDb())
            {
                try
                {


                    await db.Customers.AddAsync(new Customer
                    {
                        CustomerId = e.CustomerId,
                        Name = e.Name,
                        EmailAddress = e.EmailAddress,
                        Address = e.Address,
                        PostalCode = e.PostalCode,
                        City = e.City,
                        TelephoneNumber = e.TelephoneNumber
                    });
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine($"Skipped adding customer with customer id {e.CustomerId}.");
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(NewProductAdded e)
        {
            Console.WriteLine($"Product created: Customer Id = {e.Id}, Name = {e.Name}, Price = {e.Price}, Weight = {e.WeightKg}");

            using (var db = GetOrderDb())
            {
                try
                {
                    await db.Products.AddAsync(new Product()
                    {
                        ProductId = e.Id,
                        Name = e.Name,
                        Price = e.Price,
                        WeightKg = e.WeightKg
                    });
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine($"Skipped adding product with product id {e.Id}.");
                }
            }

            return true;
        }

        private async Task<bool> HandleAsync(OrderPayed e)
        {
            Console.WriteLine($"Order paid: Order Id = {e._orderId}, Bank = {e._bank}");

            using (var db = GetOrderDb())
            {
                try
                {
                    var order = await db.Orders.FirstOrDefaultAsync(o => o.OrderId == e._orderId);
                    if (order == null)
                    {
                        throw new KeyNotFoundException($"Order with ID {e._orderId} not found");
                    }

                    order.AddStateChange(OrderState.PAYMENTCOMPLETE);

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    Console.WriteLine($"Skipped paying order with order id {e._orderId}.");
                }
            }

            return true;
        }

        private OrderDbContext GetOrderDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);
            return new OrderDbContext(optionsBuilder.Options);
        }
    }
}
