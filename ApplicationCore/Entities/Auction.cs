using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Exceptions;
using ApplicationCore.Extensions;

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
        private readonly List<Slot> _items = new List<Slot>();
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
        
        private readonly List<Category> _categories = new List<Category>();
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        
        public Auction()
        { }
        
        public Auction(string title, DateTime startedOn, DateTime endedOn)
        {
            Id = Guid.NewGuid();
            Title = title;
            StartedOn = startedOn;
            EndedOn = endedOn;
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
        public bool HasCategory(Category ca)
        {
            // var auctionCategories = _items.Select(s => s.Category.Name).Distinct();
            return _categories.Contains(ca);
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
                _items.Add(slot);
                slot.Auction = this;
            }
        }

        public void UpdateTitle(string requestTitle)
        {
            Title = requestTitle ?? Title;
        }

        public void UpdatePeriod(in DateTime requestStartedOn, in DateTime requestEndedOn)
        {
            // TODO: refactor
            if (requestStartedOn.IsEarlierThan(DateTime.Now))
                throw new AuctionDateTimeException("Wrong time specification");
            if (requestEndedOn.IsEarlierThan(DateTime.Now))
                throw new AuctionDateTimeException("Wrong time specification");
            if (requestEndedOn.IsEarlierThan(requestStartedOn))
                throw new AuctionDateTimeException("Wrong time specification");
            
            if (DateTime.Compare(StartedOn, requestStartedOn) != 0)
            {
                StartedOn = requestStartedOn;
            }
            if (DateTime.Compare(EndedOn, requestEndedOn) != 0)
            {
                EndedOn = requestEndedOn;
            }
        }

        public void UpdateSlots(IEnumerable<Slot> requestSlots)
        {
            foreach (var slot in requestSlots)
            {
                AddSlot(slot);
            }
        }
    }
    
    //TODO: move to Exceptions/
    public class AuctionDateTimeException : Exception
    {
        public AuctionDateTimeException(string message) : base(message)
        {
        }
    }
}