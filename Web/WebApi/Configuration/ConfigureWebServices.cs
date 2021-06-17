using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.AppSettings;
using WebApi.Extensions;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Configuration
{
    public static class ConfigureWebServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<ITokenClaimsService, TokenClaimsService>();
            return services;
        }
    }
}