using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using WebApi.Common;

namespace WebApi.ApiEndpoints.SlotEndpoints
{
    public class CreateSlotRequest : BaseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid OwnerId { get; set; }
        public decimal StartPrice { get; set; }
        public List<IFormFile> Pictures { get; set; }    
    }
}