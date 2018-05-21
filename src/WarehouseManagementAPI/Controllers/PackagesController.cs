using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Commands;
using WarehouseManagementAPI.Events;
using WarehouseManagementAPI.Models;
using WarehouseManagementAPI.Repositories;

namespace WarehouseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    public class PackagesController : Controller
    {
        public IMessagePublisher _messagePublisher;
        public IPackageRepository _packageRepo;
        public IOrderRepository _orderRepo;
        public IProductRepository _productRepo;
        public ITransportRepository _transportRepo;

        public PackagesController(
            IMessagePublisher messagePublisher,
            IPackageRepository packageRepo,
            IOrderRepository orderRepo,
            IProductRepository productRepo,
            ITransportRepository transportRepo)
        {
            _transportRepo = transportRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _packageRepo = packageRepo;
            _messagePublisher = messagePublisher;
        }

        // GET api/packages
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _packageRepo.GetPackagesAsync());
        }

        // GET api/packages/5
        [HttpGet]
        [Route("{packageId}", Name = "GetPackageById")]
        public async Task<IActionResult> GetAsync(string packageId)
        {
            var package = await _packageRepo.GetPackageAsync(packageId);

            if (package != null)
            {
                return Ok(package);
            }

            return NotFound();
        }

        // POST api/packages
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegisterPackage command)
        {
            if (ModelState.IsValid)
            {
                Package package = Mapper.Map<Package>(command);

                // Checklist for package, order and product
                List<Product> products = new List<Product>();
                List<Order> orders = new List<Order>();
                bool packageIsChecked = false;
                bool orderIsChecked = false;

                foreach (var order in package.Orders)
                {
                    // Get the order from order repository
                    var ord = await _orderRepo.GetOrderAsync(order.OrderId);

                    if (ord != null)
                    {
                        // Create a "PackageOrder" entity and map the relationships with package and order 
                        var packageOrder = new PackageOrder
                        {
                            Package = package,
                            Order = ord,
                        };

                        // Add both "PackageOrder" and "Order" to the package
                        package.PackageOrders.Add(packageOrder);
                        orders.Add(ord);
                    }
                    else return NotFound();
                }

                // Check if ALL the orders have the same value in order "Destination"
                // (NOTE: Orders must have the same destination in order to be packaged)
                if (orders.Count != 0)
                {
                    orderIsChecked = !package.Orders
                        .Select(o => o.Destination)
                        .Distinct()
                        .Skip(1)
                        .Any();
                }
                else return BadRequest();

                foreach (var product in package.Products)
                {
                    // Get the product from product repository
                    var prod = await _productRepo.GetProductAsync(product.ProductId);

                    if (prod != null)
                    {
                        // Add the total weight to the package from all products
                        package.WeightInKgMax += prod.Weight;

                        // Create a "PackageProduct" entity and map the relationships with package and product
                        var packageProduct = new PackageProduct
                        {
                            Package = package,
                            Product = prod,
                        };

                        // Add both "PackageProduct" and "Product" to the package
                        package.PackageProducts.Add(packageProduct);
                        products.Add(prod);
                    }
                    else return NotFound();
                }

                // Replace the current package "Orders" and "Products" with correct ones
                if (orders.Count != 0 && products.Count != 0)
                {
                    package.Orders = orders;
                    package.Products = products;
                }

                // Check if the package is of the type "Letterbox" and its total weight is less or equal to 2
                // (NOTE: This is a "Letterbox" package)
                if (package.TypeOfPackage == "Letterbox package" && package.WeightInKgMax <= 2)
                {
                    packageIsChecked = true;
                }

                // Check if the package is NOT of type "Letterbox" and its total weight is greater then 2 and less or equal to 20
                // (NOTE: This is a "Package" of any kind)
                if (package.TypeOfPackage != "Letterbox package" && package.WeightInKgMax > 2 && package.WeightInKgMax <= 20)
                {
                    packageIsChecked = true;
                }

                if (packageIsChecked == true && orderIsChecked == true)
                {
                    // Insert Package
                    package.Region = package.Orders.Select(o => o.Destination).FirstOrDefault();
                    package.ShippingStatus = "IN STOCK";
                    package.Transport = null;
                    await _packageRepo.AddPackageAsync(package);

                    return CreatedAtRoute("GetPackageById", new { packageId = package.PackageId }, package);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // TODO: PUT api/packages/5
        // When Shipping Service receive the package change the "ShippingStatus" to "SORTING"
        // (NOTE: Shipping Statuses: "IN STOCK", "ISSUE", "SORTING", "IN TRANSIT", "DELIVERED")
        [HttpPut("{packageId}")]
        public async Task<IActionResult> PutAsync(string packageId, [FromBody] RegisterPackage command)
        {
            if (ModelState.IsValid)
            {
                var package = await _packageRepo.GetPackageAsync(packageId);

                if (package != null)
                {
                    // Shipping Checklist for package and transport
                    bool packageIsIssued = false;
                    bool transportIsChecked = false;
                    var transports = await _transportRepo.GetTransportsAsync(package.PackageId);
                    var transport = await _transportRepo.GetTransportAsync(command.Transport.TransportId);

                    // Update Package
                    // (NOTE: Only "ShippingStatus", "BarcodeNumber", "Delivered" and "Transport")
                    package.BarcodeNumber = command.BarcodeNumber;

                    // Check
                    if (package.ShippingStatus == "IN STOCK" && command.Transport != null)
                    {
                        if (transports.Count() != 0 && transport != null && transports.Any(t => t == transport))
                        {
                            package.ShippingStatus = "ISSUE";
                            package.Delivered = false;
                            package.Transport = transport;

                            packageIsIssued = true;
                            transportIsChecked = true;
                        }
                        else return NotFound();
                    }

                    await _packageRepo.UpdatePackageAsync();

                    if (packageIsIssued == true && transportIsChecked)
                    {
                        // Send Event
                        RegisterPackage c = Mapper.Map<RegisterPackage>(package);
                        PackageRegistered e = Mapper.Map<PackageRegistered>(c);
                        await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                        return CreatedAtRoute("GetPackageById", new { packageId = package.PackageId }, package);
                    }
                }

                return NotFound();
            }

            return BadRequest();
        }

        // DELETE api/packages/5
        [HttpDelete("{packageId}")]
        public async Task<IActionResult> DeleteAsync(string packageId)
        {
            var package = await _packageRepo.GetPackageAsync(packageId);

            if (package != null)
            {
                if (package.ShippingStatus == "IN STOCK")
                {
                    // Delete Package
                    // (NOTE: Only "Package" and the relationship in "PackageOrder" and "PackageProduct" will be deleted)
                    await _packageRepo.DeletePackageAsync(package);

                    return CreatedAtRoute("GetPackageById", new { packageId = package.PackageId }, package);
                }

                return BadRequest();
            }

            return NotFound();
        }
    }
}
