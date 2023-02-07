using AutoMapper;
using ShopApp.Dtos.Item;

namespace ShopApp.Mapper
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
