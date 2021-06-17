using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using IAuctionService = WebApi.Interfaces.IAuctionService;

namespace WebApi.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAsyncRepository<Auction> _auctionRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;
        private readonly IAsyncRepository<Slot> _slotRepository;

        public AuctionService(
            IAsyncRepository<Auction> auctionRepository,
            IAsyncRepository<Category> categoryRepository,
            IAsyncRepository<Slot> slotRepository)
        {
            _auctionRepository = auctionRepository;
            _categoryRepository = categoryRepository;
            _slotRepository = slotRepository;
        }
        
        //another approach
        // public async Task<AuctionDto> CreateAuctionDto(Auction auction)
        // {
        //     // var aDto = auctions.Select(_mapper.Map<AuctionDto>);
        //     // auction: Id, title, startedOn, endedOn
        //     var auctionDto = new AuctionDto
        //     {
        //         Id = auction.Id,
        //         Title = auction.Title,
        //         EndedOn = auction.EndedOn,
        //         AuctionSlotDtoItems = await GetAuctionSlots(auction.Id),
        //         CategoryNames = await GetDistinctCategoryNames(auction.Id)
        //     };
        //
        //     return auctionDto;
        // }
        // return dto form of slot
        // private async Task<IReadOnlyCollection<AuctionSlotDto>> GetAuctionSlots(Guid auctionId)
        // {
        //     var auctionSlotsSpecification = new AuctionSlotsSpecification(auctionId);
        //     var auctionSlots = await _slotRepository.ListAsync(auctionSlotsSpecification);
        //
        //     return null;
        // }

        public async Task AddSlotsToAuction(Auction auction, IEnumerable<Guid> slotIds)
        {
            foreach (var slotId in slotIds)
            {
                var slot = await _slotRepository.GetByIdAsync(slotId);
                if (slot != null)
                {
                    auction.AddSlot(slot);
                }
            }
        }

        public List<string> GetDistinctCategoryNames(Auction auction)
        {
            var categories = new HashSet<string>();
            
            foreach (var item in auction.Items)
            {    
                var itemCategoryId = item.CategoryId;
                var category = _categoryRepository.GetByIdAsync(itemCategoryId).Result; //don't do this
                categories.Add(category.Name);
            }

            return categories.ToList();
        }

        public IReadOnlyList<Auction> FilterAuctions(
            IEnumerable<Auction> auctions,
            string title,
            DateTime? start,
            DateTime? end,
            Guid? categoryId)
        {
            var result = !String.IsNullOrEmpty(title) 
                ? auctions.Where(a => a.Title.Contains(title))
                : auctions;
            
            result = categoryId.HasValue
                ? FilterAuctionsByCategory(result, categoryId)
                : result;
            
            return result
                .Where(a => 
                    (!start.HasValue || a.StartedOn >= start.Value) &&
                    (!end.HasValue || a.EndedOn <= end.Value))
                .ToList();
        }
        
        private IReadOnlyList<Auction> FilterAuctionsByCategory(IEnumerable<Auction> auctions, Guid? categoryId)
        {
            return auctions
                .Where(a => a.Items.Any(s => s.CategoryId == categoryId))
                .ToList();
        }
    }
}