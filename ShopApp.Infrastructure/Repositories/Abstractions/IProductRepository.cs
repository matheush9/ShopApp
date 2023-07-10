using ShopApp.Application.Filters;
using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IProductRepository : IRepository<Product>
    {
        new Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetProductsByIdsList(List<int> idList);
        Task<List<Product>> Filter(ProductFilter productParams, PaginationFilter paginationFilter, decimal minPrice, decimal maxPrice);        
    }
}
