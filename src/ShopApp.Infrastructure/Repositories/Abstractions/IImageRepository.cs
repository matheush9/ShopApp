using ShopApp.Domain.Entities;

namespace ShopApp.Infrastructure.Repositories.Abstractions
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<Image> GetImageByUser(int id);
    }
}
