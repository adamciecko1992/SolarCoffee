using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using solarcoffee.data;
using solarcoffee.services;
using solarcoffee.services.Product;



namespace solarcoffee.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<solarDbContext>((opts)=> {
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
            services.AddTransient<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "solarcoffee.web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
