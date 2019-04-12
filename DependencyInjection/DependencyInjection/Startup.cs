using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjection.Infrastructure;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public class Startup
    {
        private IHostingEnvironment env;

        public Startup(IHostingEnvironment hostEnv) => env = hostEnv;

        public void ConfigureServices(IServiceCollection services)
        {
            TypeBroker.SetRepositoryType<AlternateRepository>();

            //services.AddTransient<IRepository, MemoryRepository>();
            // Factory funciton to use implementation class based on hosting environment
            //services.AddTransient<IRepository>(provider => {
            //    if (env.IsDevelopment())
            //    {
            //        var x = provider.GetService<MemoryRepository>();
            //        return x;
            //    } else
            //    {
            //        return new AlternateRepository();
            //    }
            //});
            //services.AddScoped<IRepository, MemoryRepository>();
            services.AddSingleton<IRepository, MemoryRepository>(); // an instance through all application for reuses
            services.AddTransient<MemoryRepository>();
            services.AddTransient<IModelStorage, DictionaryStorage>();
            services.AddTransient<ProductTotalizer>();

            services.AddMvc();
        }
        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
