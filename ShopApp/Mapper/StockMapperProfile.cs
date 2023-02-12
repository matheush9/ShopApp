using AutoMapper;
using ShopApp.Dtos.Stock;

namespace ShopApp.Mapper
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
