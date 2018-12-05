using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazoned.AchievementHunter.DAL.InMemory;
using Blazoned.AchievementHunter.IoC.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Blazoned.AchievementHunter.AspNetCoreDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add MVC
            services.AddMvc();

            // Add Achievement Hunter
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

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
