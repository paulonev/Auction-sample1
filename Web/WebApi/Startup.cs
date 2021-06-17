using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;
using WebApi.Extensions;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = new LoggerFactory().CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);
            services.AddCoreServices(Configuration);
            services.AddWebServices(Configuration);
            services.AddAutoMapper(typeof(Startup).Assembly);
            //add authentication & authorization
            services.AddJwtAuthentication(services.BindJwtSettings(Configuration));
            services.AddCloudinary(Configuration);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000", "https://localhost:3000",
                            "http://localhost:3001", "https://localhost:3001");
                        builder.AllowCredentials();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuctionWebApi", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _logger.LogInformation("Having dev environment");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuctionWebApi v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorizationHeader();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseCors();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
