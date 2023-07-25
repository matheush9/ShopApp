using ShopApp.Application.DTOs.ProductCategory;
using ShopApp.Domain.DTOs.Category;

namespace ShopApp.Application.Interfaces.ProductCategory
{
    public interface IProductCategoryService
    {
        Task<GetProductCategoryResponseDto> GetProductCategoryById(int id);
        Task<List<GetProductCategoryResponseDto>> GetAllProductCategories();
        Task<GetProductCategoryResponseDto> AddProductCategory(AddProductCategoryRequestDTO newProductCategory);
    }
}
