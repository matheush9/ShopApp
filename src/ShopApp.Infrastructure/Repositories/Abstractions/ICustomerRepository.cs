using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByUserId(int id);
    }
}
