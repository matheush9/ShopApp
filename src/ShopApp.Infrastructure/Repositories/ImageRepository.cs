using Microsoft.EntityFrameworkCore;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(DataContext context) : base(context)
        {
        }

        public async Task<Image> GetImageByUser(int id)
        {
            return await _dbset.AsNoTracking().FirstOrDefaultAsync(i => i.UserId == id);
        }
    }
}
