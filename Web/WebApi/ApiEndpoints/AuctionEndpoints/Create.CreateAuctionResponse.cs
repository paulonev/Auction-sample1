using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class CreateAuctionResponse : BaseResponse
    {
        public CreateAuctionResponse()
        {
        }

        public CreateAuctionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public AuctionDto Auction { get; set; }
    }
}