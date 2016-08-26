///Created by: Bartul Kovačić
///Github: https:github.com/BKova
///Released under : "MIT Licence"

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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Evidencija.Auth;
using Evidencija.Encription;
using Evidencija.Middleware;

namespace Evidencija
{
    public class Startup
    {
        private IConfiguration Config { get; set; }

        private SecurityKey _RSAKey { get; set; }

        private TokenAuthenticationOptions _tokenOptions { get; set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("dbconfig.json", optional: false, reloadOnChange: false);

            builder.AddEnvironmentVariables();
            Config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().Build();

                auth.AddPolicy("Admin", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser().RequireClaim("IsAdmin", new[] { "True" }).Build());
            });

            _RSAKey = new RsaSecurityKey(RSAKeyUtils.GetRandomKey());

            _tokenOptions = new TokenAuthenticationOptions
            (
                Audience: "EvidencijaUsers",
                Issuer: "EvidencijaWebService",
                SigningCredentials: new SigningCredentials(_RSAKey, SecurityAlgorithms.RsaSha256Signature)
            );

            services.AddSignalR(options => {
                options.Hubs.EnableDetailedErrors = true;
            });

            services.AddDbContext<EvidencijaDbContext>(options => {
                options.UseSqlServer(Config["ConnectionString"]);
            });

            services.AddScoped<IDbContextBinder,DbContextBinder>();
            services.AddSingleton<UserCollection>();
            services.AddSingleton<TokenAuthenticationOptions>(_tokenOptions);
            services.AddSingleton<JwtTokenProvider>();

            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomAuthentication();

            app.UseSignalR();

            app.UseMvc();
        }
    }
}
