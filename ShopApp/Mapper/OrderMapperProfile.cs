using AutoMapper;
using ShopApp.Dtos.Order;

namespace ShopApp.Mapper
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<Order, GetOrderResponseDto>();
            CreateMap<Order, AddOrderRequestDto>();
            CreateMap<AddOrderRequestDto, Order>();
        }
    }
}
