using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using WebApi.AuctionEndpoints;

namespace WebApi.Interfaces
{
    public interface IAuctionService
    {
        // Task<AuctionDto> CreateAuctionDto(Auction auction);
        // Task<Auction> AddSlots(Auction auction);
        Task AddSlotsToAuction(Auction auction, IEnumerable<Guid> slotIds);
        List<string> GetDistinctCategoryNames(Auction auction);
        IReadOnlyList<Auction> FilterAuctionsByCategory(IEnumerable<Auction> auctions, Guid? categoryId);
    }
}    