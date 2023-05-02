using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Application.Interfaces.Store
{
    public interface IStoreService
    {
        Task<GetStoreResponseDto> GetStoreByUserId(int userId);
    }
}
