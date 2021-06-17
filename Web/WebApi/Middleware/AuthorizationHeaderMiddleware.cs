using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApi.Middleware
{
    public class AuthorizationHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Cookies.TryGetValue("accessToken", out var jwtToken);

            if (jwtToken != null)
            {
                context.Request.Headers.Append("Authorization", $"Bearer {jwtToken}");
            }

            await _next(context);
        }
    }
}