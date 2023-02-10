using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Cart;
using ShopApp.Services.CartServices;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        [Route("Customer/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCartResponseDto>>> GetCart([FromRoute] int id)
        {
            var cart = await _cartService.GetCartByCustomerId(id);

            if (cart is null) 
                return NotFound(cart);

            return Ok(cart);
        }

    }
}
