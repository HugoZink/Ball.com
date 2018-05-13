using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementAPI.Database;
using ProductManagementAPI.Infrastructure.Database;

namespace ProductManagementAPI
{
	public class Program
    {
        public static void Main(string[] args)
        {
			var host = BuildWebHost(args);

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var context = services.GetRequiredService<ProductDbContext>();
				ProductDbSeeder.Seed(context);
			}

			host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
