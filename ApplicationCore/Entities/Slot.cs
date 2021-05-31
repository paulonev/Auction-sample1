using System;
using System.Collections.Generic;
using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    /// <summary>
    /// Domain entity of app
    /// Represents unit of trade
    /// </summary>
    public class Slot
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        public Guid OwnerId { get; set; }
        
        public Trader Owner { get; set; }
        
        public Guid? AuctionId { get; set; }
        
        public Auction Auction { get; set; } // may be required by services, remove if won't use

        public decimal StartPrice { get; set; }
        
        public SlotStatus Status { get; set; }
        
        public List<Picture> Pictures { get; set; }
        
        public IReadOnlyCollection<Bid> Bids { get; set; }

        public Slot()
        { }
        
        public Slot(
            string title,
            string description,
            Guid categoryId,
            Guid ownerId,
            Guid? auctionId,
            decimal startPrice,
            List<Picture> pictures = null)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CategoryId = categoryId;
            OwnerId = ownerId;
            AuctionId = auctionId;
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

        public void AddPictureRange(IEnumerable<Picture> pictures)
        {
            foreach (var picture in pictures)
            {
                AddPicture(picture);
            }
        }
        
        public void AddPicture(Picture picture)
        {
            picture.ItemId = Id;
            Pictures.Add(picture);
        }
    }
}