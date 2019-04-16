using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersIdentity.Infrastructure;
using UsersIdentity.Models;

namespace UsersIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register a custom password validator
            services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<AppUser>, CustomUserValidator>();

            services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();

            services.AddTransient<IAuthorizationHandler, BlockUsersHandler>();
            services.AddTransient<IAuthorizationHandler, DocumentAuthorizationHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DCUsers", policy =>
                {
                    policy.RequireRole("Users");
                    policy.RequireClaim(ClaimTypes.StateOrProvince, "DC");
                });

                options.AddPolicy("NotBob", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new BlockUsersRequirement("Bob"));
                });

                options.AddPolicy("AuthorsAndEditors", policy =>
                {
                    policy.AddRequirements(new DocumentAuthorizationRequirement {
                        AllowAuthors = true,
                        AllowEditors = true
                    });
                });
            });

            // Third-Party Authentication
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "<Google Client ID>";
                options.ClientSecret = "<Google Client Secret>";
            });

            // dotnet ef migrations add Initial --project UsersIdentity
            // dotnet ef database update --project UsersIdentity
            // dotnet ef migrations add CustomProperties --project UsersIdentity
            // dotnet ef database update --project UsersIdentity
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportsStoreIdentity:ConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "abcdefghiklmnopqrstuvwxyz";

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
            services.AddMvc();

            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Users/Login";
                options.AccessDeniedPath = "/Users/AccessDenied";
            });

        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            // Seed database with first admin user in appsettings.json configuration
            // must disable the DI scope validation feature in Program.cs cause we're accessing scoped service via IApplicationBuilder.ApplicationServices
            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
