using ShopApp.Dtos.Stock;

namespace ShopApp.Services.StockServices
{
    public interface IStockService
    {
        Task<ServiceResponse<GetStockResponseDto>> GetStockByProductId(int id);
        Task<ServiceResponse<GetStockResponseDto>> UpdateStock(int id, UpdateStockRequest newStock);
        Task<ServiceResponse<List<GetStockResponseDto>>> GetAllStocksByStoreId(int id);
    }
}
