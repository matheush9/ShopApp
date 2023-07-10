using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<List<Item>> GetItemsByOrderId(int id);
        Task<List<Item>> AddItemsList(List<Item> itemsList);
    }
}
