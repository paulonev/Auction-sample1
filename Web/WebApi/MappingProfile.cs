using System.Linq;
using ApplicationCore.Entities;
using AutoMapper;
using Infrastructure.Data;
using WebApi.ApiEndpoints.AuctionEndpoints;
using WebApi.ApiEndpoints.BidEndpoints;
using WebApi.ApiEndpoints.CategoryEndpoints;
using WebApi.ApiEndpoints.Common;
using WebApi.ApiEndpoints.SlotEndpoints;
using WebApi.Interfaces;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDto>()
                .AfterMap<SetCategoryNamesForAuction>()
                .ForMember(dest => dest.AuctionSlotDtoItems, opt => opt.MapFrom(src => src.Items));
            CreateMap<Slot, AuctionSlotDto>();
            CreateMap<Slot, SlotDto>();
            CreateMap<Picture, PictureDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.PictureUri));
            CreateMap<Category, CategoryDto>();
            CreateMap<Bid, BidDto>()
                .AfterMap<SetTraderName>();
            CreateMap<CreateBidRequest, Bid>()
                .ForMember(dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.ToUniversalTime()));

        }
    }

    public class SetCategoryNamesForAuction : IMappingAction<Auction, AuctionDto>
    {
        private readonly IAuctionService _auctionService;

        public SetCategoryNamesForAuction(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        
        public void Process(Auction source, AuctionDto destination, ResolutionContext context)
        {
            destination.CategoryNames = _auctionService.GetDistinctCategoryNames(source);
        }
    }
    
    public class SetTraderName : IMappingAction<Bid, BidDto>
    {
        private readonly AuctionDbContext _dbContext;

        public SetTraderName(AuctionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Process(Bid source, BidDto destination, ResolutionContext context)
        {
            destination.TraderName = _dbContext.Users
                .Where(u => u.Id == source.TraderId.ToString())
                .Select(u => u.UserName).First();
        }
    }

}