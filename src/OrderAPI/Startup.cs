using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure;
using OrderAPI.Commands;
using OrderAPI.DataAccess;
using OrderAPI.Events;
using OrderAPI.Model;
using OrderAPI.EventHandler;
using Pitstop.Infrastructure.Messaging;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Polly;

namespace OrderAPI
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

        public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add DbContext classes
			var sqlConnectionString = Configuration.GetConnectionString("OrderManagementCN");
			services.AddDbContext<OrderDbContext>(options => {
				options.UseSqlServer(sqlConnectionString);
			});

			// Add messagepublisher classes
			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];

			services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));
			services.AddTransient<OrderDbInitializer>();

			// Add framework services.
			services.AddMvc();

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "Order API", Version = "v1" });
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, OrderDbContext dbContext, OrderDbInitializer dbInit)
        {
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();
			app.UseDefaultFiles();
			app.UseStaticFiles();

			SetupAutoMapper();

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "LogisticsManagement API - v1");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// setup messagehandler
			var configSection = Configuration.GetSection("RabbitMQ");
			string host = configSection["Host"];
			string userName = configSection["UserName"];
			string password = configSection["Password"];
			RabbitMQMessageHandler messageHandler = new RabbitMQMessageHandler(host, userName, password, "Ball.com", "OrderAPI", "");

			// start event-handler
			var eventHandler = new EventHandler.EventHandler(messageHandler, Configuration.GetConnectionString("OrderManagementCN"));
			eventHandler.Start();

			Policy
				.Handle<Exception>()
				.WaitAndRetry(10, r => TimeSpan.FromSeconds(5))
				.Execute(() =>
				{
					dbInit.Seed().Wait();
				});
		}

		private void SetupAutoMapper()
		{
			// setup automapper
			Mapper.Initialize(cfg =>
			{
				// Map CRUD Commands and Events
				cfg.CreateMap<CreateOrder, Order>();
				cfg.CreateMap<UpdateOrder, Order>();
				cfg.CreateMap<RemoveOrder, Order>();
				cfg.CreateMap<PlaceOrder, Order>();

				cfg.CreateMap<Order, CreateOrder>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<Order, UpdateOrder>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<Order, RemoveOrder>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<Order, PlaceOrder>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));

				cfg.CreateMap<CreateOrder, OrderCreated>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<UpdateOrder, OrderUpdated>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<RemoveOrder, OrderDeleted>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<PlaceOrder, OrderPlaced>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
				cfg.CreateMap<Order, OrderPlaced>()
					.ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
			});
		}
	}
}
