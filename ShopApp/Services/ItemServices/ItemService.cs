using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Item;
using ShopApp.Services.GenericService;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.ItemServices
{
    public class ItemService : IGenericService<GetItemResponseDto, AddItemRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetItemResponseDto> responseHandler = new();

        public ItemService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetItemResponseDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetItemResponseDto>();

            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetItemResponseDto>(item);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<List<GetItemResponseDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetItemResponseDto>>();
            var responseHandler = new DefaultResponseHandler<List<GetItemResponseDto>>();
            serviceResponse.Data = await _context.Items.Select(p => _mapper.Map<GetItemResponseDto>(p)).ToListAsync();
            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetItemResponseDto>> Add(AddItemRequestDto newItem)
        {
            var serviceResponse = new ServiceResponse<GetItemResponseDto>();

            _context.Items.Add(_mapper.Map<Item>(newItem));
            await _context.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemResponseDto>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<GetItemResponseDto>();
            var item = await _context.Items.FindAsync(id);

            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetItemResponseDto>(item);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetItemResponseDto>> Update(int id, AddItemRequestDto newItem)
        {
            var serviceResponse = new ServiceResponse<GetItemResponseDto>();
            var item = await _context.Items.FindAsync(id);

            if (item != null)
            {
                item.Quantity = newItem.Quantity;
                item.PriceTotal = newItem.PriceTotal;
                item.ProductId = newItem.ProductId;
                item.CartId = newItem.CartId;
                item.OrderId = newItem.OrderId;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetItemResponseDto>(item);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
