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
        
        [JsonConverter(typeof(DateTimeConverter), new object[] { "MM-dd-yyyy", "yyyyMMddHHmmss" })]        
        public DateTime StartDate { get; set; }
        
        [JsonConverter(typeof(DateTimeConverter), new object[] { "MM-dd-yyyy", "yyyyMMddHHmmss" })]        
        public DateTime EndDate { get; set; }
        
        public List<Guid> Slots { get; set; }
    }
}