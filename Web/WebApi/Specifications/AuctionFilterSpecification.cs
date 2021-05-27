using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace WebApi.Specifications
{
    public sealed class AuctionFilterSpecification : Specification<Auction>
    {
        public AuctionFilterSpecification(Guid categoryId)
        {
            Query.Where(a => a.HasCategory(categoryId));
        }
    }
}