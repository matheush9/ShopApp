using AutoMapper;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.DTOs.Order;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderResponseDto> GetById(int id)
        {
            var item = await _orderRepository.GetByIdAsync(id);

            return _mapper.Map<GetOrderResponseDto>(item);
        }

        public async Task<List<GetOrderResponseDto>> GetOrdersByCustomerId(int id)
        {
            var orders = await _orderRepository.GetOrdersByCustomerId(id);

            return _mapper.Map<List<GetOrderResponseDto>>(orders);
        }

        public async Task<GetOrderResponseDto> Add(AddOrderRequestDto newOrder)
        {
            var order = _mapper.Map<Order>(newOrder);
            await _orderRepository.AddAsync(order);
               
            return _mapper.Map<GetOrderResponseDto>(order);
        }

        public async Task<GetOrderResponseDto> Delete(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order != null)
                await _orderRepository.DeleteAsync(order);

            return _mapper.Map<GetOrderResponseDto>(order);
        }

        public async Task<GetOrderResponseDto> Update(int id, AddOrderRequestDto newOrder)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order != null)
            {
                order.CustomerId = newOrder.CustomerId;

                await _orderRepository.UpdateAsync(order);
            }

            return _mapper.Map<GetOrderResponseDto>(order);
        }
    }
}
