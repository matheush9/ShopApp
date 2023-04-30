using AutoMapper;
using ShopApp.Domain.DTOs.Category;
using ShopApp.Domain.Entities;

namespace ShopApp.Domain.Mapper
{
    public class ProductCategoryMapperProfile : Profile
    {
        public ProductCategoryMapperProfile()
        {
            CreateMap<ProductCategory, GetProductCategoryResponseDto>();
        }
    }
}
