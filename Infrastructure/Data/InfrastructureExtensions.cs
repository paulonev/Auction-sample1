using Infrastructure.Data.DataAccess;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddDataInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AuctionDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("AuctionConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
                {
                    opt.Lockout.MaxFailedAccessAttempts = 5;
                    opt.Lockout.AllowedForNewUsers = true;

                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireDigit = false;

                    opt.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AuctionDbContext>()
                .AddDefaultTokenProviders();
            // .AddTokenProvider<DigitTokenProvider>(DigitTokenProvider) // phonenumbertokenprovider inheritant

            services.AddScoped<IAuctionDbContext>(provider => provider.GetService<AuctionDbContext>());
            return services;
        }
    }
}