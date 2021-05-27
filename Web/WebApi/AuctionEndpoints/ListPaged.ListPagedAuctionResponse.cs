using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
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
    }
}