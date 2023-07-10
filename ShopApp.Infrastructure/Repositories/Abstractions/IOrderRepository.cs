using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersByCustomerId(int id);
    }
}
