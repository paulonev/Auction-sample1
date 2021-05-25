using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApiEndpointsProject.Specifications
{
    public class AuctionCatalogFilterPaginatedSpecification : Specification<Slot>
    {
        public AuctionCatalogFilterPaginatedSpecification(int skip, int take, Guid? categoryId) 
            : base()
        {
            Query.Where(s => (!categoryId.HasValue || s.CategoryId == categoryId))
                .Paginate(skip, take);
        }
    }
}