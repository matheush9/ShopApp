using AutoMapper;
using ShopApp.Dtos.Product;

namespace ShopApp.Mapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            //Product
            CreateMap<Product, GetProductResponseDto>();
            CreateMap<Product, AddProductRequestDto>();
            CreateMap<AddProductRequestDto, Product>();
        }
    }
}
