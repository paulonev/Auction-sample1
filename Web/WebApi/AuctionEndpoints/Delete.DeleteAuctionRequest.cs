using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class DeleteAuctionRequest : BaseRequest
    {
        public Guid AuctionId { get; set; }
    }
}