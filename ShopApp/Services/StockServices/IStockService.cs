using ShopApp.Dtos.Stock;

namespace ShopApp.Services.StockServices
{
    public interface IStockService
    {
        Task<GetStockResponseDto> GetStockByProductId(int id);
        Task<GetStockResponseDto> UpdateStockByProductId(int id, UpdateStockRequest newStock);
        Task<List<GetStockResponseDto>> GetAllStocksByStoreId(int id);
        Task AddStock(int productId, int storeId);
    }
}
