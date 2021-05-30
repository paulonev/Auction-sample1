using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class AuctionCoreService : IAuctionCoreService
    {
        private IAsyncRepository<Auction> _auctionRepository;
        private IAsyncRepository<Slot> _slotRepository;
        
        public AuctionCoreService(
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Slot> slotRepository)
        {
            _auctionRepository = auctionRepository;
            _slotRepository = slotRepository;
        }
        
        public async Task AddSlotToAuction(Guid auctionId, Slot slot)
        {
            var auction = await _auctionRepository.GetByIdAsync(auctionId);
            auction?.AddSlot(slot);
            // throw exception if auction is null
        }

        public async Task AddSlotsToAuction(Auction auction, IEnumerable<Guid> slotIds)
        {
            
        }
    }
}