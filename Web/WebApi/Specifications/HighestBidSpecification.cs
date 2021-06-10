using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace WebApi.Specifications
{
    public sealed class HighestBidSpecification : Specification<Bid>
    {
        public HighestBidSpecification(Guid slotId)
        {
            Query.Include(b => b.Trader).Where(b => b.SlotId == slotId);
        }
    }
}