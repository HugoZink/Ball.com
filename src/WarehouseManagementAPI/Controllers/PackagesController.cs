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
        public IProductRepository _productRepo;

        public PackagesController(
            IPackageRepository packageRepo,
            IProductRepository productRepo,
            IMessagePublisher messagePublisher)
        {
            _productRepo = productRepo;
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
                bool packageIsChecked = false;

                Package package = Mapper.Map<Package>(command);

                foreach (var product in package.Products)
                {
                    // Get the product from product repository
                    var prod = await _productRepo.GetProductAsync(product.ProductId);

                    if (prod != null)
                    {
                        // Add the total weight to the package from all products
                        package.WeightInKgMax += prod.Weight;

                        // Create and Add a "PackageProduct" entity and map the relationships with package and product
                        var packageProduct = new PackageProduct
                        {
                            Package = package,
                            Product = prod,
                        };

                        package.PackageProducts.Add(packageProduct);
                    }
                    else return NotFound();
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

                if (packageIsChecked == true)
                {
                    // Insert Package
                    await _packageRepo.AddPackageAsync(package);

                    // Send Event
                    PackageRegistered e = Mapper.Map<PackageRegistered>(command);
                    await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                    return CreatedAtRoute("GetPackageById", new { packageId = package.PackageId }, package);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // TODO: PUT api/packages/5
        // Update can only be applied when Shipping Service haven't received the package
        // When Shipping Service receive the package change the "ShippingStatus" to "SORTING"
        // (NOTE: Shipping Statuses: "IN STOCK", "ISSUE", "SORTING", "IN TRANSIT", "DELIVERED")

        // TODO: DELETE api/package/5
        // Delete can only be applied when Shipping Service haven't received the package
        // When Shipping Service receive the package change the "ShippingStatus" to "SORTING"
        // (NOTE: Shipping Statuses: "IN STOCK", "SORTING", "IN TRANSIT", "DELIVERED")
    }
}
