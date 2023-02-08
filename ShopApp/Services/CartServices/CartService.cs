using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Cart;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetCartResponseDto> responseHandler = new();

        public CartService(IMapper mapper, DataContext context) 
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCartResponseDto>> GetCartByCustomerId(int id)
        {
            var serviceResponse = new ServiceResponse<GetCartResponseDto>();
             
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CustomerId == id);
            serviceResponse.Data = _mapper.Map<GetCartResponseDto>(cart);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
