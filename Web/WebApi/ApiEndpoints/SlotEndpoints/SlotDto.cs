using System;
using System.Collections.Generic;
using ApplicationCore.Entities;
using WebApi.ApiEndpoints.Common;

namespace WebApi.ApiEndpoints.SlotEndpoints
{
    public class SlotDto
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public Guid? AuctionId { get; set; }
        
        public decimal StartPrice { get; set; }
        
        public SlotStatus Status { get; set; }
        
        public List<PictureDto> Pictures { get; set; }
        
        // public IReadOnlyCollection<Bid> Bids { get; set; }
    }
}