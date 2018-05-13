using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LogisticsManagementAPI.Commands;
using LogisticsManagementAPI.DataAccess;
using LogisticsManagementAPI.Events;
using LogisticsManagementAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pitstop.Infrastructure.Messaging;
using Swashbuckle.AspNetCore.Swagger;

namespace LogisticsManagementAPI
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
            // Add DbContext Classes
            var sqlConnectionString = Configuration.GetConnectionString("LogisticsManagementCN");
            services.AddDbContext<LogisticsManagementDbContext>(options =>
                options.UseSqlServer(sqlConnectionString));

            // Add messagepublisher classes
            var configSection = Configuration.GetSection("RabbitMQ");
            string host = configSection["Host"];
            string userName = configSection["UserName"];
            string password = configSection["Password"];

            services.AddTransient<IMessagePublisher>((sp) => new RabbitMQMessagePublisher(host, userName, password, "Ball.com"));
            services.AddTransient<LogisticsManagementDbInitializer>();

            // Add framework services.
            services.AddMvc();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "LogisticsManagement API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, LogisticsManagementDbInitializer dbInit)
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

            dbInit.Seed().Wait();
        }

        private void SetupAutoMapper()
        {
            // setup automapper
            Mapper.Initialize(cfg =>
            {
                // Map CRUD Commands and Events
                cfg.CreateMap<RegisterTransport, Transport>();
                cfg.CreateMap<UpdateTransport, Transport>();
                cfg.CreateMap<RemoveTransport, Transport>();

                cfg.CreateMap<Transport, RegisterTransport>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
                cfg.CreateMap<Transport, UpdateTransport>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
                cfg.CreateMap<Transport, RemoveTransport>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));

                cfg.CreateMap<RegisterTransport, TransportRegistered>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
                cfg.CreateMap<UpdateTransport, TransportUpdated>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
                cfg.CreateMap<RemoveTransport, TransportRemoved>()
                    .ForCtorParam("messageId", opt => opt.ResolveUsing(t => Guid.NewGuid()));
            });
        }
    }
}
