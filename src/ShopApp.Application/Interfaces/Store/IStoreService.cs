using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Application.Interfaces.Store
{
    public interface IStoreService
    {
        Task<GetStoreResponseDto> GetById(int id);
        Task<GetStoreResponseDto> GetStoreByUserId(int storeId);
        Task<GetStoreResponseDto> Add(AddStoreRequestDto newStore);
        Task<GetStoreResponseDto> Delete(int id);
        Task<GetStoreResponseDto> Update(int id, AddStoreRequestDto newStore);
    }
}
