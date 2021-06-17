using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class GetBidsBySlotIdResponse : BaseResponse
    {
        public GetBidsBySlotIdResponse(Guid correlationId) : base(correlationId)
        {
        }

        public List<BidDto> Bids { get; set; }
        public int ItemsFetched { get; set; }
    }
}