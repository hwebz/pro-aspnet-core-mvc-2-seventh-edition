using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IProductRepository, FakeProductRepository>(); // similar with Ninject, every time we call IProductRepository, instance of FakeProductRepository will serve

            // Using Database
            // dotnet ef migrations add Initial => to add migration to database using entity framework
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();

            // Using app identity
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            // session storage service
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Order service
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc();
            // enable in-memory data store
            services.AddMemoryCache();
            // enabling sessions
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")
                    ),
                    RequestPath = new PathString("/vendor")
                });
                app.UseDeveloperExceptionPage(); // display details of exceptions that occur
                app.UseStatusCodePages(); // add a simple method to HTTP responses such as 404 - Not Found
            } else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles(); // enable support for serving static content from wwwroot folder
            app.UseMvc(routes => // enable ASP.NET Core MVC
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List" }
                );

                routes.MapRoute(
                    name: "pagination",
                    template: "Page{productPage:int}",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                //routes.MapRoute(
                //    name: null,
                //    template: "Category/{category}",
                //    defaults: new {controller = "Product", action = "List", productPage = 1}
                //);

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List", productPage = 1 }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=List}/{id?}"
                );
            });

            // using seed features in AdminController when user hit Seed Database instead of auto-seed at the first time app running
            // and indentity as well when user navigate to Account/Login route, Identity Seed will be seeded
            //SeedData.EnsurePopulated(app);
            //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
