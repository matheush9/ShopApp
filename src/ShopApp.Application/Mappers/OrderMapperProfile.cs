using AutoMapper;
using ShopApp.Domain.DTOs.Order;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
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
