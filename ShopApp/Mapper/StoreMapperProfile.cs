using AutoMapper;
using ShopApp.Dtos.Store;

namespace ShopApp.Mapper
{
    public class StoreMapperProfile : Profile
    {
        public StoreMapperProfile()
        {
            CreateMap<Store, GetStoreResponseDto>();
            CreateMap<Store, AddStoreRequestDto>();
            CreateMap<AddStoreRequestDto, Store>();
        }
    }
}
