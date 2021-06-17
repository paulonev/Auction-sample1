using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public interface IAuctionDbContext
    {
        DbSet<Auction> Auctions { get; set; }
        DbSet<Slot> Slots { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Bid> Bids { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Trade> Trades { get; set; }
        DbSet<Trader> Traders { get; set; }
    }
}