using System;
using System.Collections.Generic;
using System.Linq;
using Admin.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Admin
{
    public class Startup
    {
        private readonly IReadOnlyList<string> _types = new List<string> { "source", "router", "proxy", "import" };

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

            services.AddSingleton<IStatRouterService, StatRouterService>(f => new StatRouterService(propDict));
            services.AddSingleton<IConfig, Config>(f => new Config(_types));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
