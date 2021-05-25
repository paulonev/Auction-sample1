using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.ViewModels.Auction;

namespace ApiEndpointsProject.ViewModels.Auction
{
    public class AuctionCatalogViewModel
    {
        public List<AuctionViewModel> Auctions { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public Guid? CategoryApplied { get; set; }
        public PaginationInfoViewModel PaginationInfo { get; set; }
    }
}