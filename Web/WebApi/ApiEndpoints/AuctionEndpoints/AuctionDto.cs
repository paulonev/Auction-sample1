using System;
using System.Collections.Generic;

namespace WebApi.ApiEndpoints.AuctionEndpoints
{
    public class AuctionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime EndedOn { get; set; }
        public IReadOnlyCollection<AuctionSlotDto> AuctionSlotDtoItems { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}