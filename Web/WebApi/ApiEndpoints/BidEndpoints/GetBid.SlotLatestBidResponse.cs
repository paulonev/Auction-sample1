using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class SlotLatestBidResponse : BaseResponse
    {
        public SlotLatestBidResponse(Guid correlationId) : base(correlationId)
        {
        }

        public BidDto Bid { get; set; }
    }
}