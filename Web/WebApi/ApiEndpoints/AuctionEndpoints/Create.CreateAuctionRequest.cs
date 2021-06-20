using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using WebApi.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class CreateAuctionRequest : BaseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }
        
        public List<Guid> Slots { get; set; }
    }
}