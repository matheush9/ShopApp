using ShopApp.Domain.DTOs.Item;

namespace ShopApp.Application.Interfaces.Item
{
    public interface IItemService
    {
        Task<List<GetItemResponseDto>> GetItemsByOrderId(int id);
    }
}
