using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        new Task<User> GetByIdAsync(int id);
        Task<User> GetUserByEmail(string email);
    }
}
