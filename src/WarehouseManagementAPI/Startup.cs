using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pitstop.Infrastructure.Messaging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using WarehouseManagementAPI.Commands;
using WarehouseManagementAPI.DataAccess;
using WarehouseManagementAPI.Events;
using WarehouseManagementAPI.Models;
using WarehouseManagementAPI.Repositories;
using WarehouseManagementAPI.Repositories.Implementations;

namespace WarehouseManagementAPI
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
            // Add dbcontext classes
            var sqlConnectionString = Configuration.GetConnectionString("WarehouseManagementCN");
            services.AddDbContext<WarehouseManagementDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            // Add repository classes
            services.AddTransient<IPackageRepository>((sp) =>
               new EFPackageRepository(sp.GetService<WarehouseManagementDbContext>()));
            services.AddTransient<IOrderRepository>((sp) =>
                new EFOrderRepository(sp.GetService<WarehouseManagementDbContext>()));
            services.AddTransient<ICustomerRepository>((sp) =>
                new EFCustomerRepository(sp.GetService<WarehouseManagementDbContext>()));
            services.AddTransient<IProductRepository>((sp) =>
                new EFProductRepository(sp.GetService<WarehouseManagementDbContext>()));
            services.AddTransient<ITransportRepository>((sp) =>
                new EFTransportRepository(sp.GetService<WarehouseManagementDbContext>()));

            // Add messagepublisher classes
            var configSection = Configuration.GetSection("RabbitMQ");
            string host = configSection["Host"];
            string userName = configSection["UserName"];
            string password = configSection["Password"];

            // Setup messagepublisher
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

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }

        private void SetupAutoMapper()
        {
            // Setup automapper
            Mapper.Initialize(cfg =>
            {
                // Map CRUD Commands and Events
                cfg.CreateMap<RegisterPackage, Package>();

                cfg.CreateMap<Package, RegisterPackage>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(p => Guid.NewGuid()));

                cfg.CreateMap<RegisterPackage, PackageRegistered>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(p => Guid.NewGuid()));
            });
        }
    }
}
