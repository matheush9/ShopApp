using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetAllProductCategories();
    }
}
