using Infrastructure.Data;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuctionDbContext>(opt =>
            {
                var dbConfig = configuration["ASPNETCORE_DB"];
                if (dbConfig.Contains("sqlite"))
                {
                    opt.UseSqlite(configuration.GetConnectionString("AuctionConnectionDev"));
                }
                else
                {
                    opt.UseSqlServer(
                        configuration.GetConnectionString("AuctionConnection"),
                        builder => builder.EnableRetryOnFailure(5));
                }
            });

            //Microsoft.AspNetCore.Identity v2.2.0
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                })
                .AddEntityFrameworkStores<AuctionDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IAuctionDbContext>(provider => provider.GetService<AuctionDbContext>());
            return services;
        }
    }
}