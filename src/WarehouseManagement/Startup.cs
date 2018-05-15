using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pitstop.Infrastructure.Messaging;
using Swashbuckle.AspNetCore.Swagger;
using WarehouseManagement.DataAccess;
using WarehouseManagement.Repositories;
using WarehouseManagement.Repositories.Implementations;

namespace WarehouseManagement
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

        public IMessageHandler MessageHandler { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext classes
            var sqlConnectionString = Configuration.GetConnectionString("WarehouseManagementCN");
            services.AddDbContext<WarehouseManagementDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            // Add repository classes
            services.AddTransient<ICustomerRepository>((sp) =>
                new CustomerRepository(sqlConnectionString));
            services.AddTransient<IProductRepository>((sp) =>
                new ProductRepository(sqlConnectionString));
            services.AddTransient<ITransportRepository>((sp) =>
                new TransportRepository(sqlConnectionString));

            // Add messagepublisher classes
            var configSection = Configuration.GetSection("RabbitMQ");
            string host = configSection["Host"];
            string userName = configSection["UserName"];
            string password = configSection["Password"];

            // setup messagehandler and messagepublisher
            MessageHandler = new RabbitMQMessageHandler(host, userName, password, "Ball.com", "WarehouseManagement", "");
            services.AddTransient<IMessagePublisher>((sp) =>
                new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));

            // Add framework services.
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WarehouseManagement API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WarehouseManagement API - v1");
            });

            // Start warehouse manager
            Manager manager = new Manager(MessageHandler);
            manager.Start();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

            }
        }

        private void SetupAutoMapper()
        {

        }
    }
}
