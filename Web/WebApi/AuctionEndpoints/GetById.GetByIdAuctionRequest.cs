using System;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class GetByIdAuctionRequest : BaseRequest
    {
        public Guid AuctionId { get; set; }
    }
}    