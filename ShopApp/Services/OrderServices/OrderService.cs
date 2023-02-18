using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Order;
using ShopApp.Services.GenericService;

namespace ShopApp.Services.OrderServices
{
    public class OrderService : IGenericService<GetOrderResponseDto, AddOrderRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public OrderService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetOrderResponseDto> GetById(int id)
        { 
            var item = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetOrderResponseDto>(item);
        }

        public async Task Add(AddOrderRequestDto newOrder)
        {
            _context.Orders.Add(_mapper.Map<Order>(newOrder));
            await _context.SaveChangesAsync();
        }

        public async Task<GetOrderResponseDto> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetOrderResponseDto>(order);
        }

        public async Task<GetOrderResponseDto> Update(int id, AddOrderRequestDto newOrder)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                order.Status = newOrder.Status;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetOrderResponseDto>(order);
        }
    }
}
