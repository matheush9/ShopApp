using Microsoft.EntityFrameworkCore;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbset;

        public Repository(DataContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbset.Update(entity);
            await _context.SaveChangesAsync();
            return entity;  
        }
    }
}
