using System;
using System.Reflection;
using ApplicationCore.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DataAccess
{
    public class AuctionDbContext : IdentityDbContext<ApplicationUser>, IAuctionDbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Trader> Traders { get; set; }

        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            modelBuilder.Entity<ApplicationUser>()
                .Property(au => au.TraderId)
                .IsRequired();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}