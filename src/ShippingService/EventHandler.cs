using System;
using System.Threading.Tasks;
using Pitstop.Infrastructure.Messaging;
using ShippingService.Events;
using ShippingService.Models;
using ShippingService.Repositories;

namespace ShippingService
{
    public class EventHandler : IMessageHandlerCallback
    {
        private IMessageHandler _messageHandler;
        private ICustomerRepository _customerRepository;
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ILogisticsRepository _logisticsRepository;

        public EventHandler(IMessageHandler messageHandler, ICustomerRepository customerRepository,
            IOrderRepository orderRepository, IProductRepository productRepository,
            ILogisticsRepository logisticsRepository)
        {
            _messageHandler = messageHandler;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logisticsRepository = logisticsRepository;
        }

        public async Task<bool> HandleMessageAsync(MessageTypes messageType, string message)
        {
            var messageObject = MessageSerializer.Deserialize(message);

            switch (messageType)
            {
                case MessageTypes.Unknown:
                    break;
                case MessageTypes.CustomerRegistered:
                    await HandleAsync(messageObject.ToObject<CustomerRegistrated>());
                    break;
                case MessageTypes.TransportRegistered:
                    await HandleAsync(messageObject.ToObject<TransportRegistered>());
                    break;
                case MessageTypes.TransportUpdated:
                    await HandleAsync(messageObject.ToObject<TransportUpdated>());
                    break;
                case MessageTypes.TransportRemoved:
                    await HandleAsync(messageObject.ToObject<TransportUpdated>());
                    break;
                case MessageTypes.NewProductAdded:
                    await HandleAsync(messageObject.ToObject<NewProductAdded>());
                    break;
                case MessageTypes.ProductUpdated:
                    await HandleAsync(messageObject.ToObject<ProductUpdated>());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(messageType), messageType, null);
            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true;
        }

        private async Task<bool> HandleAsync(CustomerRegistrated e)
        {
            Console.WriteLine($"Customer registered: Customer Id = {e.Id}, Name = {e.Name}, Adress = {e.Adress}");

            var newCustomer = new Customer {Id = e.Id, Name = e.Name, Adress = e.Adress};

            await _customerRepository.CreateAsync(newCustomer);

            return true;
        }

        private async Task<bool> HandleAsync(NewProductAdded e)
        {
            var newProduct = new Product {ProductId = e.Id, Name = e.Name};

            await _productRepository.CreateAsync(newProduct);

			Console.WriteLine($"Product added: Id = {e.Id}, Name = {e.Name}");

			return true;
        }

        private async Task<bool> HandleAsync(ProductUpdated e)
        {
            Console.WriteLine($"Product updated: Id = {e.Id}, Name = {e.Name}");

            var newProduct = new Product { ProductId = e.Id, Name = e.Name};

            await _productRepository.UpdateAsync(newProduct);

            return true;
        }

        private async Task<bool> HandleAsync(TransportRegistered e)
        {
            Console.WriteLine($"Logistics added: Id = {e.TransportId}, Name = {e.CompanyName}");

            var newTransport = new Logistics
            {
                Id = e.TransportId,
                Name = e.CompanyName,
                CountryOfDestination = e.CountryOfDestination,
                ShippingCost = e.ShippingCost,
                TypeOfShipment = e.TypeOfShipment,
                WeightInKgMax = e.WeightInKgMax
            };

            await _logisticsRepository.CreateAsync(newTransport);

            return true;
        }

        private async Task<bool> HandleAsync(TransportRemoved e)
        {
            Console.WriteLine($"Logistics removed: Id = {e.TransportId}");
            await _logisticsRepository.RemoveAsync(e.TransportId);

            return true;
        }

        private async Task<bool> HandleAsync(TransportUpdated e)
        {
            Console.WriteLine($"Logistics updated: Id = {e.TransportId}");

            var newTransport = new Logistics
            {
                Id = e.TransportId,
                Name = e.CompanyName,
                CountryOfDestination = e.CountryOfDestination,
                ShippingCost = e.ShippingCost,
                TypeOfShipment = e.TypeOfShipment,
                WeightInKgMax = e.WeightInKgMax
            };

            await _logisticsRepository.UpdateAsync(newTransport);

            return true;
        }

//        public void Start()
//        {
//            _messageHandler.Start(this);
//        }
//
//        public void Stop()
//        {
//            _messageHandler.Stop();
//        }
    }
}