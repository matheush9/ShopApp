using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Stock>> GetAllStocksByStoreId(int id)
        {
            return await _dbset.AsNoTracking().Where(s => s.StoreId == id).ToListAsync();
        }

        public async Task<Stock> GetStockByProductId(int id)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id); 
        }
    }
}
