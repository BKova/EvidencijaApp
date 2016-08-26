///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
using Evidencija.Auth;
using System;

namespace Evidencija.Middleware
{
    public class AuthentificationMiddleware
    {
        private RequestDelegate _next;

        private JwtTokenProvider _provider;

        public AuthentificationMiddleware(RequestDelegate next, JwtTokenProvider provider)
        {
            _provider = provider;

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("api"))
            {
                await _next(context);
                return;
            }

            else if (context.Request.Path.Value.Contains("login"))
            {
                await _next(context);
                return;
            }

            else if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 403;
                return;
            }

            else if (!context.Request.Headers["Authorization"].ToString().Contains("Bearer "))
            {
                context.Response.StatusCode = 403;
                return;
            }

            var Token = context.Request.Headers["Authorization"].ToString().Split(new char[] {' '})[1];

            try
            {
                var ClaimsPrincipal = _provider.Validate(Token);
                context.User = ClaimsPrincipal;
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 403;
                return;
            }
            

            await _next(context);

            return;
        }
    }
}
