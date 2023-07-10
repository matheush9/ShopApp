using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Order>> GetOrdersByCustomerId(int id)
        {
            return await _dbset.Where(o => o.CustomerId == id).ToListAsync();
        }
    }
}
