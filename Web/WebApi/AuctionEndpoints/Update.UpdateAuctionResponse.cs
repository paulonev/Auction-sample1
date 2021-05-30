using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class UpdateAuctionResponse : BaseResponse
    {
        public UpdateAuctionResponse(Guid correlationId) : base(correlationId)
        {
        }
        
        public AuctionDto Auction { get; set; }
    }
}