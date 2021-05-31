using Microsoft.Extensions.Configuration;

namespace WebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetCloudinaryCloudName(this IConfiguration configuration)
        {
            return configuration.GetSection("Cloudinary:CloudName").Value;
        }
        
        public static string GetCloudinaryApiKey(this IConfiguration configuration)
        {
            return configuration.GetSection("Cloudinary:ApiKey").Value;
        }
        
        public static string GetCloudinaryApiSecret(this IConfiguration configuration)
        {
            return configuration.GetSection("Cloudinary:ApiSecret").Value;
        }
    }
}