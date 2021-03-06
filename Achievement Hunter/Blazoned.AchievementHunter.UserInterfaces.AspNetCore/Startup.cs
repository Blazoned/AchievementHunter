﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazoned.AchievementHunter.DAL.InMemory;
using Blazoned.AchievementHunter.IoC.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Blazoned.AchievementHunter.UserInterfaces.AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Setup Autofac
            ContainerBuilder builder = new ContainerBuilder();

            // Add ASP.NET Core-registered services (in this case just Mvc) to the Autofac container builder.
            builder.Populate(services);

            // Add the Achievement Hunter library to the builder and build the container. (This automatically configures the data acces layer)
            IContainer container = AchievementHunterServiceManager.BuildContainer(builder, typeof(ConnectionInMemory).Assembly.Location, typeof(DAL.Configuration.ConfigurationDAL).Assembly.Location);

            // Return the DI container for the web application
            return new AutofacServiceProvider(container);
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
            app.UseMvc();
        }
    }
}
