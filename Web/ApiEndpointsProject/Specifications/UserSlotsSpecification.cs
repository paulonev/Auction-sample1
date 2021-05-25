using System;
using ApplicationCore.Entities;
using Ardalis.Specification;

namespace ApiEndpointsProject.Specifications
{
    public class UserSlotsSpecification : Specification<Slot>
    {
        public UserSlotsSpecification(Guid userId)
        {
            Query.Where(s => s.OwnerId == userId);
        }
    }
}