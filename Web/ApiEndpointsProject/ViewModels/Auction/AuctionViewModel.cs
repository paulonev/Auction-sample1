using System;
using System.Collections.Generic;
using WebApi.ViewModels.Slot;

namespace WebApi.ViewModels.Auction
{
    public class AuctionViewModel
    {
        public string Id { get; set; } //Guid -> string (20.05)
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Category { get; set; }
        public string CreatorName { get; set; }
        public List<SlotViewModel> Slots { get; set; }
    }
}