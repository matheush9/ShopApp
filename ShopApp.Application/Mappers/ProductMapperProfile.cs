using AutoMapper;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
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
