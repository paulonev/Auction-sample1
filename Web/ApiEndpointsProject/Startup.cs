using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Data.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            // ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }

        private void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<AuctionDbContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("AuctionConnection"));
            });
            
            services.AddDbContext<AppIdentityDbContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });
            
            ConfigureServices(services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // cookie configuration
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });
            
            // inject identity service
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
                
            // inject token claim service
            services.AddScoped<ITokenClaimsService, TokenClaimsService>();

            // inject EF Core services
            services.AddDataInfrastructure(Configuration);
            
            // inject MVC pattern
            services.AddControllersWithViews();
        }
        
        // private void ConfigureInMemoryDatabases(IServiceCollection services)
        // {
        //     services.AddDbContext<AuctionDbContext>(c =>
        //         c.UseInMemoryDatabase("AuctionContext"));
        //     
        //     ConfigureServices(services);
        // }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
