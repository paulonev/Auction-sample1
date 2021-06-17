using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.ApiEndpoints.CategoryEndpoints
{
    public class ListCategoriesResponse : BaseResponse
    {
        public ListCategoriesResponse(Guid correlationId) : base(correlationId)
        {
        }

        public List<CategoryDto> Categories { get; set; }
    }
}