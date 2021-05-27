using System;
using System.Collections.Generic;
using WebApi.Common;

namespace WebApi.AuctionEndpoints
{
    public class CreateAuctionRequest : BaseRequest
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> Slots { get; set; }
    }
}