using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class ListPagedAuctionRequest : BaseRequest
    {
        public int PageIndex { get; set; } = AppConstants.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = AppConstants.ITEMS_PER_PAGE;
        
        public string Title { get; set; }
        public Guid? CategoryId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}