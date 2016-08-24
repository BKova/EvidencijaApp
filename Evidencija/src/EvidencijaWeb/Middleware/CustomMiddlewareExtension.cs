using Microsoft.AspNetCore.Builder;

namespace Evidencija.Middleware
{
    public static class CustomAuthenticationExtension
    {
        public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthentificationMiddleware>();
        }
    }
}
