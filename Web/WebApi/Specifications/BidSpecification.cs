using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace WebApi.Specifications
{
    public sealed class BidSpecification : Specification<Bid>
    {
        public BidSpecification(Guid slotId)
        {
            Query
                .Where(b => b.SlotId == slotId)
                .Include(b => b.Trader);
        }
        
        public BidSpecification(Guid slotId, bool highestBid=true)
        {
            Query
                .Where(b => b.SlotId == slotId)
                .Include(b => b.Trader)
                .OrderBy(b => (double)b.Amount);
        }
    }
}