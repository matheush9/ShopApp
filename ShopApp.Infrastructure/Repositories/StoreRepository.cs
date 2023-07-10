using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        public StoreRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<Store>> GetAll()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<Store> GetStoreByUserId(int userId)
        {
            return await _dbset.FirstOrDefaultAsync(s => s.UserId == userId);
        }
    }
}
