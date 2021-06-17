using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class CreateBidResponse : BaseResponse
    {
        public CreateBidResponse(Guid correlationId) : base(correlationId)
        {
        }

        public List<BidDto> Bids { get; set; }
    }
}