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
        OrderDbContext _dbContext;
        IMessageHandler _messageHandler;

        public EventHandler(IMessageHandler messageHandler, OrderDbContext dbContext)
        {
            _messageHandler = messageHandler;
            _dbContext = dbContext;
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
                }
            }
            catch(Exception ex)
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

            try
            {
                await _dbContext.Customers.AddAsync(new Customer
                {
                    CustomerId = e.CustomerId,
                    Name = e.Name,
                    TelephoneNumber = e.TelephoneNumber
                });
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                Console.WriteLine($"Skipped adding customer with customer id {e.CustomerId}.");
            }

            return true; 
        }
    }
}
