using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using solarcoffee.data;
using solarcoffee.services;
using solarcoffee.services.Product;
using solarcoffee.services.Customer;
using solarcoffee.services.Inventory;
using solarcoffee.services.Order;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.IO;
using solarcoffee.services.GlobalErrorHandler;
using solarcoffee.services.GlobalErrorHandler.Extensions;
using AutoMapper;

namespace solarcoffee.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson((opts) => opts.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });

            services.AddDbContext<solarDbContext>((opts) =>
            {
                opts.EnableDetailedErrors();
                //zajrzy do appsettings.development.json i znajdzie tam connection string do postgresql
                opts.UseNpgsql(Configuration.GetConnectionString("solar.dev"));

            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "solarcoffee.web", Version = "v1" });
            });
            //za kazdym razem gdy bede potrzebowal IPorductService daj mi instancje ProductService
            //Injection in framework, zawsze daje nowa instancje kiedy jest potrzebny
            //Taki serrvice lifetime, uzywany dla statless services bo sa lekkie, ejsli mielibysmy jakas 
            //gruba usluge to dobrze by bylo stworzyc jedna instancje i wykorzystywac ja wielokrotnie, ale moze to prowadzic,
            //do nieoczekiwanych zachowan
            services.AddSingleton<ILoggerManager, LoggerMenager>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "solarcoffee.web v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
               builder => builder
                   .WithOrigins(
                       "http://localhost:8080",
                       "http://localhost:8081",
                       "http://localhost:8082")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
               );

            app.UseAuthorization();
            app.UseExceptionHandler("/error");
            app.ConfigureExceptionHandler(logger);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });




        }

    }
}
