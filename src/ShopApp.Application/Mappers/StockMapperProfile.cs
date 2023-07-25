using AutoMapper;
using ShopApp.Domain.DTOs.Stock;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class StockMapperProfile : Profile
    {
        public StockMapperProfile()
        {
            CreateMap<Stock, GetStockResponseDto>();
            CreateMap<Stock, UpdateStockRequest>();
            CreateMap<UpdateStockRequest, Stock>();
        }
    }
}
