using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Trader
    {
        // a reference guid to Asp.Net Core Identity entity
        public Guid Id { get; set; }
        public string NickName { get; set; }
        
        private readonly List<Bid> _bids = new List<Bid>();
        public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();
        
        private readonly List<Slot> _createdSlots = new List<Slot>();
        public IReadOnlyCollection<Slot> CreatedSlots => _createdSlots.AsReadOnly();
        
        /// <summary>
        /// Auctions that user has created
        /// Slot: auctionId - creatorId(Trader)
        /// Ignored in ef core
        /// </summary>
        private readonly List<Auction> _createdAuctions = new List<Auction>();
        public IReadOnlyCollection<Auction> CreatedAuctions => _createdAuctions.AsReadOnly();
    }
}