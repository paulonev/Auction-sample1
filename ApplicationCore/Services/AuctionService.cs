using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class AuctionService : IAuctionService
    {
        private IAsyncRepository<Auction> _auctionRepository;
        private IAsyncRepository<Category> _categoryRepository;

        public AuctionService(
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Category> categoryRepository)
        {
            _auctionRepository = auctionRepository;
            _categoryRepository = categoryRepository;
        }
        
        // decide upon q
        public async Task AddSlotToAuction(Guid auctionId, Slot slot)
        {
            var auction = await _auctionRepository.GetByIdAsync(auctionId);
            auction?.AddSlot(slot);
            // throw exception if auction is null
        }

        public async Task<List<string>> GetDistinctCategoryNames(Guid auctionId)
        {
            var categories = new HashSet<string>();
            var auction = await _auctionRepository.GetByIdAsync(auctionId);
            
            foreach (var item in auction.Items)
            {    
                var itemCategoryId = item.CategoryId;
                var category = await _categoryRepository.GetByIdAsync(itemCategoryId);
                categories.Add(category.Name);
            }

            return categories.ToList();
        }
    }
}