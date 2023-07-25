using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<List<Stock>> GetAllStocksByStoreId(int id);
        Task<Stock> GetStockByProductId(int id);
    }
}
