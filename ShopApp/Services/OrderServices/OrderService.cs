using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Order;
using ShopApp.Services.GenericService;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.OrderServices
{
    public class OrderService : IGenericService<GetOrderResponseDto, AddOrderRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetOrderResponseDto> responseHandler = new();

        public OrderService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetOrderResponseDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetOrderResponseDto>();

            var item = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetOrderResponseDto>(item);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetOrderResponseDto>> Add(AddOrderRequestDto newOrder)
        {
            var serviceResponse = new ServiceResponse<GetOrderResponseDto>();

            _context.Orders.Add(_mapper.Map<Order>(newOrder));
            await _context.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetOrderResponseDto>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<GetOrderResponseDto>();
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetOrderResponseDto>(order);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetOrderResponseDto>> Update(int id, AddOrderRequestDto newOrder)
        {
            var serviceResponse = new ServiceResponse<GetOrderResponseDto>();
            var order = await _context.Orders.FindAsync(id);

            if (order != null)
            {
                order.Status = newOrder.Status;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetOrderResponseDto>(order);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
