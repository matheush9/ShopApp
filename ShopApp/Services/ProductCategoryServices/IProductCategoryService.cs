using ShopApp.Dtos.Category;

namespace ShopApp.Services.CategoryServices
{
    public interface IProductCategoryService
    {
        Task<GetProductCategoryResponseDto> GetProductCategoryById(int id);
    }
}
