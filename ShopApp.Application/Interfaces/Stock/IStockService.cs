using ShopApp.Domain.DTOs.Stock;

namespace ShopApp.Application.Interfaces.Stock
{
    public interface IStockService
    {
        Task<GetStockResponseDto> GetStockByProductId(int id);
        Task<GetStockResponseDto> UpdateStockByProductId(int id, UpdateStockRequest newStock);
        Task<List<GetStockResponseDto>> GetAllStocksByStoreId(int id);
        Task AddStock(int productId, int storeId);
    }
}
