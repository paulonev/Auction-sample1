using System;
using System.IO;
using System.Reflection;
using Infrastructure.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AuctionDbContext>
    {
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public AuctionDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + "../../Web";
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));            
        }

        private AuctionDbContext Create(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                // .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("AuctionConnection");

            return Create(connectionString);
        }
        
        private AuctionDbContext Create(string connectionString)
        {
            // if (string.IsNullOrEmpty(connectionString))
            // {
            //     throw new ArgumentException($"Connection string '{connectionString}' is null or empty.",
            //         nameof(connectionString));
            // }

            // Console.WriteLine(
            //     $"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<AuctionDbContext>();

            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(5);
            });

            return new AuctionDbContext(optionsBuilder.Options);
        }
    }
}