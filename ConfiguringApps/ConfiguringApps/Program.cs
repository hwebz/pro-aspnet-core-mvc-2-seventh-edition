using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConfiguringApps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseStartup<Startup>();
                .UseStartup(nameof(ConfiguringApps));

        //return new WebHostBuilder()
        //      // Use Kestrel to run web application
        //    .UseKestrel()
        //      // Use wwwroot directory to serve html, css, js files
        //    .UseContentRoot(Directory.GetCurrentDirectory())
        //      // Load conguration files to configure our app
        //.ConfigureAppConfiguration((hostingContext, config) => {
        //    var env = hostingContext.HostingEnvironment;
        //    config.AddJsonFile("appsettings.json",
        //            optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
        //            optional: true, reloadOnChange: true);
        //    config.AddEnvironmentVariables();
        //    if (args != null)
        //    {
        //        config.AddCommandLine(args);
        //    }
        //})
        //      // Use logging system for development
        //    .ConfigureLogging((hostingContext, logging) => {
        //        logging.AddConfiguration(
        //            hostingContext.Configuration.GetSection("Logging"));
        //        logging.AddConsole();
        //        logging.AddDebug();
        //    })              
        //      // use IIS integration server
        //    .UseIISIntegration()
        //      // Use Dependency Injection
        //    .UseDefaultServiceProvider((context, options) => {
        //         options.ValidateScopes =
        //         context.HostingEnvironment.IsDevelopment();
        //     })  
        //    .UseStartup<Startup>()
        //    .Build();
    }
}
