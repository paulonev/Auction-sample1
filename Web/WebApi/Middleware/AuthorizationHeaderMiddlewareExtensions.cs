using Microsoft.AspNetCore.Builder;

namespace WebApi.Middleware
{
    public static class AuthorizationHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationHeaderMiddleware>();
        }
    }
}