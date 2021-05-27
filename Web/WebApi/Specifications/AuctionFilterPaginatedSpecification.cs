using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace WebApi.Specifications
{
    public sealed class AuctionFilterPaginatedSpecification : Specification<Auction>
    {
        // add min price - max price range
        public AuctionFilterPaginatedSpecification(int skip, int take, Guid? categoryId)
        {
            Query
                .Where(a => categoryId.HasValue || a.HasCategory(categoryId.Value))
                .Include(a => a.Items)
                .Paginate(skip, take);
        }
    }
}