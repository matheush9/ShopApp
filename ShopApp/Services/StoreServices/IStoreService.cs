using ShopApp.Dtos.Store;

namespace ShopApp.Services.StoreServices
{
    public interface IStoreService
    {
        Task<ServiceResponse<List<GetStoreResponseDto>>> GetAllStores();
        Task<ServiceResponse<GetStoreResponseDto>> GetStoreById(int id);
        Task<ServiceResponse<GetStoreResponseDto>> AddStore(AddStoreRequestDto newStore);
        Task<ServiceResponse<GetStoreResponseDto>> UpdateStore(int id, AddStoreRequestDto newStore);
        Task<ServiceResponse<GetStoreResponseDto>> DeleteStore(int id);
    }
}
    