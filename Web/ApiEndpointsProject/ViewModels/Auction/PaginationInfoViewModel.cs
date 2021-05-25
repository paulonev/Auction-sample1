using System;

namespace WebApi.ViewModels.Auction
{
    public class PaginationInfoViewModel
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages => int.Parse(Math.Ceiling((decimal) TotalItems / ItemsPerPage).ToString());
        
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}