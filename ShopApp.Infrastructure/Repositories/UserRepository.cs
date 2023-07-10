using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbset.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbset
              .Include(i => i.Images)
              .Include(s => s.Store)
              .Include(c => c.Customer)
              .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
