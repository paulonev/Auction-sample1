using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class UpdateAuctionRequest : BaseRequest
    {
        public Guid AuctionId { get; set; }
        
        public string Title { get; set; }
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public List<Guid> Slots { get; set; } 
    }
}