using ShopApp.Domain.Common;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IRepository<TEntity>   
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
