using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class SlotLatestBidRequest : BaseRequest
    {
        public Guid SlotId { get; set; }
    }
}