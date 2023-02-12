using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Stock;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetStockResponseDto> responseHandler = new();

        public StockService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetStockResponseDto>>> GetAllStocksByStoreId(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStockResponseDto>>();
            var responseHandler = new DefaultResponseHandler<List<GetStockResponseDto>>();

            var stocks = await _context.Stocks.Where(s => s.StoreId == id).ToListAsync();
            serviceResponse.Data = stocks.Select(_mapper.Map<GetStockResponseDto>).ToList();

            return responseHandler.SetResponse(serviceResponse);
        }
        
        public async Task<ServiceResponse<GetStockResponseDto>> GetStockByProductId(int id)
        {
            var serviceResponse = new ServiceResponse<GetStockResponseDto>();

            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == id);
            serviceResponse.Data = _mapper.Map<GetStockResponseDto>(stock);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetStockResponseDto>> UpdateStockByProductId(int id, UpdateStockRequest newStock)
        {
            var serviceResponse = new ServiceResponse<GetStockResponseDto>();
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == id);

            if (stock != null)
            {
                stock.AvailableQuantity= newStock.AvailableQuantity;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetStockResponseDto>(stock);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
