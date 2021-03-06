using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestFarm.Models;

namespace TestFarm
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<VerticalFarmingContext>(
                options => options.UseSqlServer(_config.GetConnectionString("VerticalFarmingDBConnection")));

            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();
            services.AddScoped<ICropRepository, DbCropRepository>();
            services.AddScoped<IPlantRepository, DbPlantRepository>();
            services.AddScoped<ITowerRepository, DbTowerRepository>();
            services.AddScoped<ILstPlantTypeRepository, DbLstPlantTypeRepository>();
            services.AddScoped<ILstPlantSizeRepository, DbLstPlantSizeRepository>();
            services.AddScoped<ILstTowerTypeRepository, DbLstTowerTypeRepository>();
            services.AddScoped<ILstLocationsRepository, DbLstLocationsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "pagination",
                    template: "Plants/Page{plantPage}",
                    defaults: new { Controller = "Plant", action = "Index" });
                routes.MapRoute(
                    "default", "{controller=Home}/{action=Index}/{id?}");
            });
            
            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
