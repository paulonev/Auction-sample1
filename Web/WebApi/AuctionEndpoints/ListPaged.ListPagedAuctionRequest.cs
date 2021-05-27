using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class ListPagedAuctionRequest : BaseRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid CategoryId { get; set; }
    }
}