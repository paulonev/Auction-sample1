using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.AppSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static JwtSettings BindJwtSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // bind to JwtSettings Model from JwtSettings.cs
            var jwtSectionConfiguration = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSectionConfiguration);
            return jwtSectionConfiguration.Get<JwtSettings>();
        }
        
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);

            services
                .AddAuthorization()
                .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });
            //RequireHttpsMetadata=false only for DEV environment
            return services;
        }
    
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
    }
}