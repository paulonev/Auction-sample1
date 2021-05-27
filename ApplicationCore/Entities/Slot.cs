using System;
using System.Collections.Generic;
using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Domain entity of app
    /// Represents unit of trade
    /// </summary>
    public class Slot : AuditableEntity
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Guid CategoryId { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public Trader Owner { get; set; }
        
        public Guid? AuctionId { get; set; }
        
        public Auction Auction { get; set; } // may be required by services, remove if won't use

        public decimal StartPrice { get; set; }
        
        public SlotStatus Status { get; set; }
        
        public List<Picture> Pictures { get; set; }
        
        // private readonly List<Bid> _bids = new List<Bid>();
        // public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();

        public Slot()
        { }
        
        public Slot(
            string title,
            string description,
            Guid categoryId,
            Guid ownerId,
            decimal startPrice,
            List<Picture> pictures)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CategoryId = categoryId;
            OwnerId = ownerId;
            StartPrice = startPrice;
            Status = SlotStatus.OnUserModification;
            Pictures = pictures ?? new List<Picture>();
        }

        public void UpdateDetails(string title, string description, decimal price)
        {
            Title = title;
            Description = description;
            StartPrice = price;
        }
        
        public void UploadPicture(Picture pic)
        {
            Pictures.Add(pic);
        }
    }
}