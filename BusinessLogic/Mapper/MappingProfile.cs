using AutoMapper;
using Models;
using Models.DTOs;

namespace BusinessLogic.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FlatDTO, Flat>();
            CreateMap<Flat, FlatDTO>();

            CreateMap<FlatImage, FlatImageDTO>().ReverseMap();
            CreateMap<HomeImage, HomeImageDTO>().ReverseMap();

            CreateMap<HomepageInfoDTO, HomepageInfo>();
            CreateMap<HomepageInfo, HomepageInfoDTO>();

            CreateMap<InfoBlockDTO, InfoBlock>();
            CreateMap<InfoBlock, InfoBlockDTO>();

            CreateMap<OrderDetails, OrderDetailsDTO>().ForMember(x => x.FlatDTO, options => options.MapFrom(y => y.Flat));
            CreateMap<OrderDetailsDTO, OrderDetails>();
        }
    }
}
