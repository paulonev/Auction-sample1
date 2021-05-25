using System;
using System.Threading.Tasks;
using ApiEndpointsProject.ViewModels.Auction;
using WebApi.ViewModels.Auction;

namespace ApiEndpointsProject.Interfaces
{
    public interface IAuctionCatalogService
    {
        // IAsyncRepository<Slot> SlotRepository { get; set; }

        // filter by category for now
        Task<AuctionCatalogViewModel> GetAuctions(int pageNumber, int pageSize, Guid? categoryId);
        // Task<IEnumerable<Slot>> GetViewedSlots(); // with authorized access
    }
}