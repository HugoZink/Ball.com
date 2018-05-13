using LogisticsManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogisticsManagementAPI.DataAccess
{
    public class LogisticsManagementDbInitializer
    {
        private LogisticsManagementDbContext _dbContext;

        public LogisticsManagementDbInitializer(LogisticsManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            _dbContext.Database.EnsureCreated();

            if (!_dbContext.Transports.Any())
            {
                var transports = new Transport[]
                {
                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Letterbox package", WeightInKgMax = 2,
                        ShippingCost = 3.80m},
                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package", WeightInKgMax = 2,
                        ShippingCost = 6.50m},

                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package", WeightInKgMax = 5,
                        ShippingCost = 6.50m},
                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package registered", WeightInKgMax = 5,
                        ShippingCost = 8.15m},

                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package with insurance", WeightInKgMax = 10,
                        ShippingCost = 14.45m},
                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package with payment", WeightInKgMax = 10,
                        ShippingCost = 18.35m},

                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package with urgent (12 hours)", WeightInKgMax = 20,
                        ShippingCost = 30.45m},
                    new Transport { CompanyName = "PostNL", TypeOfShipment = "Package",
                        CountryOfDestination = "Netherlands", Description = "Package with urgent (10 hours)", WeightInKgMax = 20,
                        ShippingCost = 42.95m},
                };

                _dbContext.AddRange(transports);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
