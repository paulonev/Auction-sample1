using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IAuctionRepository : IAsyncRepository<Auction>
    {
        Task AddSlotAsync(Slot slot);

        Task<Auction> GetByIdWithSlots(string id);
    }
}