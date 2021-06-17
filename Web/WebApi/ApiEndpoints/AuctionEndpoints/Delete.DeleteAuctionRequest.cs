using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class DeleteAuctionRequest : BaseRequest
    {
        public Guid AuctionId { get; set; }
    }
}