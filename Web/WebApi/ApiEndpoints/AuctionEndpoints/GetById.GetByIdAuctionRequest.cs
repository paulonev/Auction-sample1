using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class GetByIdAuctionRequest : BaseRequest
    {
        public Guid AuctionId { get; set; }
    }
}    