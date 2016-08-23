using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Evidencija.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Evidencija.Database.Models;
using Evidencija.Hubs;

namespace Evidencija
{
    public class Startup
    {
        private IConfiguration Config { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("dbconfig.json", optional: false, reloadOnChange: false);

            builder.AddEnvironmentVariables();
            Config = builder.Build();

            if(env.IsDevelopment()) Config["ConnectionString"] += "";
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options => {
                options.Hubs.EnableDetailedErrors = true;
            });

            services.AddDbContext<EvidencijaDbContext>(options => {
                options.UseSqlServer(Config["ConnectionString"]);
            });

            services.AddScoped<IDbContextBinder,DbContextBinder>();
            services.AddSingleton<UserCollection>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR("/signalr");

            app.Use(async (context, next) =>
            {
                if(context.Request.Path.Value.Contains("check")) await context.Response.WriteAsync("OK");

                else await next();
            });
        }
    }
}
