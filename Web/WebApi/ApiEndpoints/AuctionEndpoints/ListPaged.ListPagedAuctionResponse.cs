using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class ListPagedAuctionResponse : BaseResponse
    {
        public ListPagedAuctionResponse()
        {
        }
        
        public ListPagedAuctionResponse(Guid correlationId) : base(correlationId)
        {
        }
        
        public List<AuctionDto> Auctions { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int ResponseItemsCount { get; set; }
    }
}