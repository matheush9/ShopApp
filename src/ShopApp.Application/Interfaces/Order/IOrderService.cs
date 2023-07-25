using ShopApp.Domain.DTOs.Order;

namespace ShopApp.Application.Interfaces.Order
{
    public interface IOrderService
    {
        Task<GetOrderResponseDto> GetById(int id);
        Task<List<GetOrderResponseDto>> GetOrdersByCustomerId(int id);
        Task<GetOrderResponseDto> Add(AddOrderRequestDto newOrder);
        Task<GetOrderResponseDto> Delete(int id);
        Task<GetOrderResponseDto> Update(int id, AddOrderRequestDto newOrder);
    }
}
