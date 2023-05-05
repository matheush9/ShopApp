using ShopApp.Domain.DTOs.Order;

namespace ShopApp.Application.Interfaces.Order
{
    public interface IOrderService
    {
        Task<List<GetOrderResponseDto>> GetOrdersByCustomerId(int id);
    }
}
