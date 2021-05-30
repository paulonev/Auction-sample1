using System.Threading.Tasks;
using ApplicationCore.Entities;
using AutoMapper;
using WebApi.AuctionEndpoints;
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
            CreateMap<Picture, PictureDto>();
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
}