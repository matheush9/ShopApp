using ShopApp.Domain.DTOs.Stock;
using System.Threading.Tasks;

namespace ShopApp.Application.Interfaces.Stock
{
    public interface IStockService
    {
        Task<GetStockResponseDto> GetStock(int id);
        Task<GetStockResponseDto> GetStockByProductId(int id);
        Task<GetStockResponseDto> UpdateStockByProductId(int id, UpdateStockRequest newStock);
        Task<List<GetStockResponseDto>> GetAllStocksByStoreId(int id);
        Task AddStock(int productId, int storeId);
    }
}
