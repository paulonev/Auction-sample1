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
        public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<CloudinaryOptions>(options =>
                {
                    options.CloudName = configuration.GetCloudinaryCloudName();
                    options.ApiKey = configuration.GetCloudinaryApiKey();
                    options.ApiSecret = configuration.GetCloudinaryApiSecret();
                });

            return services;    
        }
        
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISlotService, SlotService>();
            services.AddScoped<IAuctionService, AuctionService>();
            return services;
        }
    }
}