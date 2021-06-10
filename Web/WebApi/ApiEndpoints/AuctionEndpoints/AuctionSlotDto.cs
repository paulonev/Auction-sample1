using System;
using System.Collections.Generic;
using WebApi.ApiEndpoints.Common;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class AuctionSlotDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public Guid CategoryId { get; set; }
        public string OwnerName { get; set; }
        public List<PictureDto> Pictures { get; set; }
    }
}