using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.BidEndpoints
{
    public class CreateBidRequest : BaseRequest
    {
        public Guid TraderId { get; set; }
        public Guid SlotId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}