using System;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Represents a buying request for a specific auction
    /// </summary>
    public class Bid
    {
        public Guid Id { get; set; }
        
        public Guid TraderId { get; set; }
        public Trader Trader { get; set; }
        
        public Guid SlotId { get; set; } // fully-defined relationship BID-SLOT
        public Slot Slot { get; set; }
        
        // 1-to-1 with Trade
        public Trade Trade { get; set; }

        public decimal Amount { get; set; }
    
        public DateTime Date { get; set; }

        public Bid()
        { }
        
        public Bid(
            Guid traderId,
            Guid slotId,
            decimal amount,
            DateTime date)
        {
            Id = Guid.NewGuid();
            TraderId = traderId;
            SlotId = slotId;
            Amount = amount;
            Date = date;
        }
        
        //some logic
    }
}