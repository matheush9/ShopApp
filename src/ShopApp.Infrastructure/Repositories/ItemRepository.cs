using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Item>> GetItemsByOrderId(int id)
        {
            return await _dbset.AsNoTracking().Where(i => i.OrderId == id).ToListAsync();
        }

        public async Task<List<Item>> AddItemsList(List<Item> itemsList)
        {
            _context.Items.AddRange(itemsList);
            await _context.SaveChangesAsync();

            return itemsList;
        }
    }
}
