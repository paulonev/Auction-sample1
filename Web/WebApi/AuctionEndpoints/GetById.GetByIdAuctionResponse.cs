using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class GetByIdAuctionResponse : BaseResponse
    {
        public GetByIdAuctionResponse()
        {
        }

        public GetByIdAuctionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public AuctionDto Auction { get; set; }
    }
}