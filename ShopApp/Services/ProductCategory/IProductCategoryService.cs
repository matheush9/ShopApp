using ShopApp.Dtos.Category;

namespace ShopApp.Services.CategoryService
{
    public interface IProductCategoryService
    {
        Task<GetProductCategoryResponseDto> GetProductCategoryById(int id);
    }
}
