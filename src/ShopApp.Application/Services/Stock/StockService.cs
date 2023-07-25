using AutoMapper;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Domain.DTOs.Stock;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;

        public StockService(IMapper mapper, IStockRepository stockRepository)
        {
            _mapper = mapper;
            _stockRepository = stockRepository;
        }

        public async Task<GetStockResponseDto> GetStock(int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            return _mapper.Map<GetStockResponseDto>(stock);
        }

        public async Task<List<GetStockResponseDto>> GetAllStocksByStoreId(int id)
        {
            var stocks = await _stockRepository.GetAllStocksByStoreId(id);

            return stocks.Select(_mapper.Map<GetStockResponseDto>).ToList();
        }

        public async Task<GetStockResponseDto> GetStockByProductId(int id)
        {
            var stock = await _stockRepository.GetStockByProductId(id);

            return _mapper.Map<GetStockResponseDto>(stock);
        }

        public async Task AddStock(int productId, int storeId)
        {
            var stock = new Stock(productId, storeId);
            await _stockRepository.AddAsync(stock);
        }

        public async Task<GetStockResponseDto> UpdateStockByProductId(int id, UpdateStockRequest newStock)
        {
            var stock = await _stockRepository.GetStockByProductId(id);

            if (stock != null)
            {
                stock.AvailableQuantity = newStock.AvailableQuantity;

                await _stockRepository.UpdateAsync(stock);
            }

            return _mapper.Map<GetStockResponseDto>(stock);
        }
    }
}
