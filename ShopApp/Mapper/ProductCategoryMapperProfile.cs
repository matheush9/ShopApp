using AutoMapper;
using ShopApp.Dtos.Category;

namespace ShopApp.Mapper
{
    public class ProductCategoryMapperProfile : Profile
    {
        public ProductCategoryMapperProfile()
        {
            CreateMap<ProductCategory, GetProductCategoryResponseDto>();
        }
    }
}
