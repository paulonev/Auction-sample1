using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApiEndpointsProject.Specifications
{
    public class AuctionCatalogFilterSpecification : Specification<Auction>
    {
        public AuctionCatalogFilterSpecification(Guid? categoryId)
        {
            Query.Where(a => (!categoryId.HasValue || a.HasCategory(categoryId.Value)));
        }   
    }
}