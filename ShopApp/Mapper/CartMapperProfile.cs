using AutoMapper;
using ShopApp.Dtos.Cart;

namespace ShopApp.Mapper
{
    public class CartMapperProfile : Profile
    {
        public CartMapperProfile()
        {
            CreateMap<Item, GetCartResponseDto>();
        }
    }
}
