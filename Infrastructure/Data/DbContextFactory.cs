using System;
using System.IO;
using System.Reflection;
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
            var basePath = $"{Directory.GetCurrentDirectory()}../../Web/WebApi";
            return Create(args[0], basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));            
        }

        private AuctionDbContext Create(string dbTypeArgument, string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)    
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                // .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

            string connectionString = "";
            string connectionType = "";
            if(dbTypeArgument.Contains("Dev"))
            {
                 connectionString = configuration.GetConnectionString("AuctionConnectionDev");
                 connectionType = "local";
            }
            else
            {
                connectionString = configuration.GetConnectionString("AuctionConnection");
                connectionType = "azure";
            }
            
            return Create(connectionString, connectionType);
        }
        
        private AuctionDbContext Create(string connectionString, string type)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{connectionString}' is null or empty.",
                    nameof(connectionString));
            }

            Console.WriteLine(
                $"DesignTimeDbContextFactory.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<AuctionDbContext>();

            optionsBuilder = type == "local" ?
                optionsBuilder.UseSqlite(connectionString) :
                optionsBuilder.UseSqlServer(connectionString, builder => builder.EnableRetryOnFailure(5));

            return new AuctionDbContext(optionsBuilder.Options);
        }
    }
}