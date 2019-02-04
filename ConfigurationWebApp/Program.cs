using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ConfigurationWebApp.Business;
using ConfigurationWebApp.Business.Impl;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharpRepository.Ioc.Autofac;

namespace ConfigurationWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIISIntegration()
               .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>();



        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    return new WebHostBuilder()

        //        .ConfigureAppConfiguration((builderContext, config) =>
        //        {
        //            var env = builderContext.HostingEnvironment;

        //            config.SetBasePath(Directory.GetCurrentDirectory());
        //            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        //            if (env.IsDevelopment())
        //            {
        //                var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
        //                if (appAssembly != null)
        //                {
        //                    config.AddUserSecrets(appAssembly, optional: true);
        //                }
        //            }
        //            config.AddEnvironmentVariables();
        //            if (args != null)
        //            {
        //                config.AddCommandLine(args);
        //            }
        //            Configuration = config.Build();
        //            var section = Configuration.GetSection("SharpRepository");

        //        })
        //        .UseKestrel(options =>
        //        {
        //            var hostIp = Configuration.GetValue<string>("hostIpAddress");
        //            var hostPort = Configuration.GetValue<int>("hostPort");
        //            options.Listen(IPAddress.Parse(hostIp), hostPort);
        //        })
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
        //            .ReadFrom.Configuration(hostingContext.Configuration))
        //        .Build();
        //}
    }
}
