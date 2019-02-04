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
using ConfigurationWebApp.Common;
using ConfigurationWebApp.Controllers;
using ConfigurationWebApp.Data;
using ConfigurationWebApp.Data.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharpRepository.Ioc.Autofac;
using SharpRepository.Repository.Ioc;

namespace ConfigurationWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }

       
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddControllersAsServices();
            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddSingleton<IConfigurationRepository, ConfigurationRepository>();

            // Create the container builder.
            var builder = new ContainerBuilder();
           

            // Register dependencies, populate the services from
            // the collection, and build the container.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            
            builder.Populate(services);
            InjectDependencies(builder);
            RepositoryDependencyResolver.SetDependencyResolver(new AutofacDependencyResolver(ApplicationContainer));
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        private static void InjectDependencies(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ConfigurationController>().PropertiesAutowired().InstancePerLifetimeScope();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );
            app.UseMvc();
        }
    }
}
