using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Cart;

namespace ShopApp.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartService(IMapper mapper, DataContext context) 
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetCartResponseDto> GetCartByCustomerId(int id)
        {    
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CustomerId == id);           

            return _mapper.Map<GetCartResponseDto>(cart); 
        }
    }
}
