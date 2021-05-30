using System;
using System.Linq;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace WebApi.Specifications
{
    public sealed class AuctionIncludeSpecification : Specification<Auction>
    {
        public AuctionIncludeSpecification()
        {
            Query
                .Include(a => a.Items)
                .ThenInclude(s => s.Category);
        }

        public AuctionIncludeSpecification(Guid auctionId)
        {
            Query
                .Include(a => a.Items)
                .ThenInclude(s => s.Category)
                .Where(a => a.Id == auctionId);
        }
    }
}