using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuctionRepository :  EfRepository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AuctionDbContext dbContext) : base(dbContext)
        {
        }
        
        public Task AddSlotAsync(Slot slot)
        {
            throw new System.NotImplementedException();
        }

        // for comparing orm standard api with Specification api
        public async Task<IReadOnlyList<Auction>> ListAuctionWithSlots(
            int skip,
            int take,
            Category category,
            CancellationToken cancellationToken)
        {
            return await _dbContext.Auctions
                .Include(a => a.Items)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

        }
    }
}