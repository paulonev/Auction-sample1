using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Exceptions;

namespace ApplicationCore.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        
        // modify model
        public string Title { get; set; }
        
        /// <summary>
        /// Items to be traded on an auction
        /// </summary>
        private readonly List<Slot> _items;
        public IReadOnlyCollection<Slot> Items => _items.AsReadOnly();

        /// <summary>
        /// Bids been made while trading the auction
        /// Ignored in ef core
        /// </summary>
        private readonly List<Bid> _bids;
        public IReadOnlyCollection<Bid> Bids => _bids.AsReadOnly();
        
        /// <summary>
        /// Start and finish of auction
        /// </summary>
        public DateTime StartedOn { get; set; }
        public DateTime EndedOn { get; set; }
        
        private readonly List<Category> _categories;
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        
        public Auction()
        { }
        
        public Auction(string title, DateTime startedOn, DateTime endedOn)
        {
            Id = Guid.NewGuid();
            Title = title;
            StartedOn = startedOn;
            EndedOn = endedOn;
            _items = new List<Slot>();
        }
        
        // Auction Status property
        
        // public void CloseAuction()
        // {
        //     foreach (var item in _items)
        //     {
        //         var itemLastBid = _bids.LastOrDefault(b => b.SlotId == item.Id.ToString());
        //         if (itemLastBid != null)
        //         {
        //             //create order
        //         }
        //         else
        //         {
        //             throw new Exception("Item not sold exception");           
        //         }   
        //     }
        // }

        /// <summary>
        /// Identifies category for auction. Returns true for presence, false - for absence of category
        /// specified for auction
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public bool HasCategory(Guid categoryId)
        {
            var auctionCategories = _items.Select(s => s.CategoryId).Distinct();
            return auctionCategories.Contains(categoryId);
        }
       
        public Bid FindWinningBid(Slot slot)
        {
            if (!_bids.Any())
                return null;
            
            return _bids.LastOrDefault(b => b.SlotId == slot.Id);
        }

        public void AddSlot(Slot slot)
        {
            if (slot == null)
            {
                throw new AddNullSlotException(nameof(AddSlot));
            }

            if (_items.All(s => s.Id != slot.Id))
            {
                slot.Auction = this;
                _items.Add(slot);
            }
        }
    }
}