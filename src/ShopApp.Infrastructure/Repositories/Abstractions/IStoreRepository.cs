using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<List<Store>> GetAll();
        Task<Store> GetStoreByUserId(int userId);
    }
}