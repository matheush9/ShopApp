using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.DTOs.Order;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.OrderServices
{
    public class OrderService : IOrderService
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

        public async Task<List<GetOrderResponseDto>> GetOrdersByCustomerId(int id)
        {
            var orders = await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();

            return _mapper.Map<List<GetOrderResponseDto>>(orders);
        }

        public async Task<GetOrderResponseDto> Add(AddOrderRequestDto newOrder)
        {
            var order = _mapper.Map<Order>(newOrder);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
               
            return _mapper.Map<GetOrderResponseDto>(order);
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
                order.CustomerId = newOrder.CustomerId;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetOrderResponseDto>(order);
        }
    }
}
