using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class ListPagedAuctionRequest : BaseRequest
    {
        public int PageIndex { get; set; } = AppConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = AppConstants.ITEMS_PER_PAGE;
        public Guid CategoryId { get; set; }
    }
}