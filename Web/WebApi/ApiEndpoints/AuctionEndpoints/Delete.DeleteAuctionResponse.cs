using System;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class DeleteAuctionResponse : BaseResponse
    {
        public DeleteAuctionResponse()
        {
        }

        public DeleteAuctionResponse(Guid correlationId) : base(correlationId)
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}