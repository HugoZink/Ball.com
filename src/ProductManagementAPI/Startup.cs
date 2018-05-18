using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pitstop.Infrastructure.Messaging;
using Polly;
using ProductManagementAPI.Database;
using ProductManagementAPI.Infrastructure.Commands;
using ProductManagementAPI.Infrastructure.Database;
using ProductManagementAPI.Infrastructure.Events;
using ProductManagementAPI.Models;
using ProductManagementAPI.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace ProductManagementAPI
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// add DBContext classes
			var sqlConnectionString = Configuration.GetConnectionString("ProductManagementCN");
			services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(sqlConnectionString));

			// add messagepublisher classes
			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));
			services.AddTransient<IProductRepository, EFProductRepository>();
			services.AddTransient<ProductDbSeeder>();

			services.AddMvc();

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "ProductManagement API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ProductDbSeeder seeder)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();


			SetupAutoMapper();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement API - v1");
			});

			Policy
				.Handle<Exception>()
				.WaitAndRetry(10, r => TimeSpan.FromSeconds(5))
				.Execute(() =>
				{
					seeder.Seed().Wait();
				});
		}

		private void SetupAutoMapper()
		{
			// setup automapper
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<AddProduct, Product>();
				cfg.CreateMap<UpdateProduct, Product>();
				cfg.CreateMap<AddProduct, NewProductAdded>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(c => Guid.NewGuid()));
				cfg.CreateMap<UpdateProduct, ProductUpdated>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(c => Guid.NewGuid()));
			});
		}
	}
}
