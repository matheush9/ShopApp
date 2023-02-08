using ShopApp.Dtos.Cart;

namespace ShopApp.Services.CartServices
{
    public interface ICartService
    {
        Task<ServiceResponse<GetCartResponseDto>> GetCartByCustomerId(int id);
    }
}
