using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Domain.DTOs.Stock;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StockService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetStockResponseDto> GetStock(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetStockResponseDto>(stock);
        }

        public async Task<List<GetStockResponseDto>> GetAllStocksByStoreId(int id)
        {
            var stocks = await _context.Stocks.Where(s => s.StoreId == id).ToListAsync();

            return stocks.Select(_mapper.Map<GetStockResponseDto>).ToList();
        }

        public async Task<GetStockResponseDto> GetStockByProductId(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == id);

            return _mapper.Map<GetStockResponseDto>(stock);
        }

        public async Task AddStock(int productId, int storeId)
        {
            var stock = new Stock(productId, storeId);
            _context.Stocks.Add(stock);

            await _context.SaveChangesAsync();
        }

        public async Task<GetStockResponseDto> UpdateStockByProductId(int id, UpdateStockRequest newStock)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == id);

            if (stock != null)
            {
                stock.AvailableQuantity = newStock.AvailableQuantity;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetStockResponseDto>(stock);
        }
    }
}
