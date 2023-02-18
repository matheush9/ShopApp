using ShopApp.Dtos.Cart;

namespace ShopApp.Services.CartServices
{
    public interface ICartService
    {
        Task<GetCartResponseDto> GetCartByCustomerId(int id);
    }
}
