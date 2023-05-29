using AutoMapper;
using ShopApp.Application.DTOs.ProductCategory;
using ShopApp.Domain.DTOs.Category;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class ProductCategoryMapperProfile : Profile
    {
        public ProductCategoryMapperProfile()
        {
            CreateMap<ProductCategory, GetProductCategoryResponseDto>();
            CreateMap<ProductCategory, AddProductCategoryRequestDTO>();
            CreateMap<AddProductCategoryRequestDTO, ProductCategory>();
        }
    }
}
