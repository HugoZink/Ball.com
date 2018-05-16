using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ShippingService.Infrastructure.Database
{
    public static class DbInitializer
    {
        public static void Initialize(ShippingDbContext context)
        {
			context.Database.EnsureCreated();
			context.Database.Migrate();
        }
    }
}