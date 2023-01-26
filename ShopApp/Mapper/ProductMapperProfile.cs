using AutoMapper;
using ShopApp.Dtos.Products;

namespace ShopApp.Mapper
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, GetProductResponseDto>();
            CreateMap<Product, AddProductRequestDto>();
            CreateMap<AddProductRequestDto, Product>();
        }
    }
}
