using AutoMapper;
using ShopApp.Domain.DTOs.Store;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
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
