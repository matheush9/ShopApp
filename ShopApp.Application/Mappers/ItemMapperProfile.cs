using AutoMapper;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class ItemMapperProfile : Profile
    {
        public ItemMapperProfile()
        {
            CreateMap<Item, GetItemResponseDto>();
            CreateMap<Item, AddItemRequestDto>();
            CreateMap<AddItemRequestDto, Item>();
        }
    }
}
