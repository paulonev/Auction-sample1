using ApplicationCore.Entities;
using AutoMapper;
using WebApi.AuctionEndpoints;

namespace WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDto>()
                .ForMember(dest => dest.AuctionSlotDtoItems, opt => opt.MapFrom(src => src.Items));
            CreateMap<Slot, AuctionSlotDto>();
            CreateMap<Picture, PictureDto>();
        }
    }
}