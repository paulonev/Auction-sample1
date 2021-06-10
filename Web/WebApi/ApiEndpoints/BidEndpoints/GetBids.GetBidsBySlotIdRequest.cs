using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class GetBidsBySlotIdRequest : BaseRequest
    {
        public Guid SlotId { get; set; }
    }
}