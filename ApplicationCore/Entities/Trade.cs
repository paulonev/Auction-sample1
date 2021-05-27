using System;

namespace ApplicationCore.Entities
{
    public class Trade
    {
        public Guid Id { get; set; }
        public Guid BidId { get; set; }
  
        public Trade(Guid bidId)
        {
            Id = Guid.NewGuid();
            BidId = bidId;
        }
    }
}