using ShopApp.Domain.DTOs.Item;

namespace ShopApp.Application.Interfaces.Item
{
    public interface IItemService
    {
        Task<GetItemResponseDto> GetById(int id);
        Task<List<GetItemResponseDto>> GetItemsByOrderId(int id);
        Task<GetItemResponseDto> Add(AddItemRequestDto newItem);
        Task<GetItemResponseDto> Delete(int id);
        Task<GetItemResponseDto> Update(int id, AddItemRequestDto newItem);
    }
}
