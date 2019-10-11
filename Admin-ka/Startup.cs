using System;
using System.IO;
using System.Linq;
using Admin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;

namespace Admin
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var propDict = Configuration
                .GetSection("ServiceUris")
                .GetChildren()
                .ToDictionary(f => f.Key, g => new Uri(g.Value));

            var watermarks = Configuration.GetSection("Watermarks");

            var watermarkInQ = long.Parse(watermarks["inQ"]);
            var watermarkSpeed = long.Parse(watermarks["speed"]);
            var refreshInterval = TimeSpan.FromMilliseconds(long.Parse(Configuration["RefreshInterval"]));

            var config = new Config(propDict.Keys.ToList(), watermarkInQ, watermarkSpeed, refreshInterval);
            services.AddSingleton<IConfig, Config>(f => config);
            services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, StatService>();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/lib")),
                RequestPath = new PathString("/lib")
            });

            app.UseSignalR(routes => routes.MapHub<AdminHub>("/adminHub"));
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }

    public class AdminHub : Hub
    {

    }
}
