using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfiguringApps
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // for Staging, Production
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc().AddMvcOptions(options => {
                options.RespectBrowserAcceptHeader = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{

        //    if ((Configuration.GetSection("ShortCircuitMiddleware")?.GetValue<bool>("EnableBrowserShortCircuit")).Value)
        //    {
        //        //// Request-Editing Middleware
        //        //app.UseMiddleware<BrowserTypeMiddleware>();
        //        //// Short-Circuiting Middleware
        //        //app.UseMiddleware<ShortCircuitMiddleware>();
        //    }

        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();

        //        //// Custom middleware
        //        //// Response-Editing Middleware
        //        //app.UseMiddleware<ErrorMiddleware>();
        //        //// Request-Editing Middleware
        //        //app.UseMiddleware<BrowserTypeMiddleware>();
        //        //// Short-Circuiting Middleware
        //        //app.UseMiddleware<ShortCircuitMiddleware>();
        //        //// Content Middleware
        //        //app.UseMiddleware<ContentMiddleware>();

        //        app.UseStatusCodePages();
        //        //app.UseBrowserLink();
        //    } else
        //    {
        //        app.UseExceptionHandler("/Home/Error");
        //    }

        //    app.UseStaticFiles();

        //    // app.UseMvc(); // same as app.UseMvcWithDefaultRoute()
        //    app.UseMvc(routes =>
        //    {
        //        routes.MapRoute(
        //            name: "default",
        //            template: "{controller=Home}/{action=Index}/{id?}"
        //        );
        //    });
        //}

        // for Development 
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddSingleton<UptimeService>();
            services.AddMvc();
        }

        // for Staging, Production
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        // for Development 
        public void ConfigureDevelopment(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            //app.UseBrowserLink();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
