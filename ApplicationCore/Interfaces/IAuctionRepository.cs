using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IAuctionRepository
    {
        Task AddSlotAsync(Slot slot);

        Task<IReadOnlyList<Auction>> ListAuctionWithSlots(
            int skip,
            int take,
            Category category,
            CancellationToken cancellationToken);

    }
}