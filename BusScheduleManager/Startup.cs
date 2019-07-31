
using BusSchedulemanager.DataAccess.Repositories;
using BusSchedulemanager.DataAccess.Repositories.Contracts;
using BusSchedulemanager.Database;
using BusSchedulemanager.Types;
using BusScheduleManager.Queries;
using BusScheduleManager.Schema;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BusScheduleManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //When registered as transient, this will automatica lly create a new instance to every controller or service.
            services.AddTransient<IBusStopRepository, BusStopRepository>();
            services.AddTransient<IBusRouteRepository, BusRouteRepository>();

            services.AddSingleton<BusManagerDbContext>(/*options => options.UseSqlServer(Configuration["ConnectionString"])*/);

            //Singleton services using dependency injection.
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<BusRouteQuery>();
            services.AddSingleton<BusRouteType>();

            var serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new BusManagerSchema(new FuncDependencyResolver(type => serviceProvider.GetService(type))));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseGraphiQl();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "Views")),
                RequestPath = "/Views"
            });
            
        }
    }
}
