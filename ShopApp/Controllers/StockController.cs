using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Domain.DTOs.Stock;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("store/{id}")]
        public async Task<ActionResult<List<GetStockResponseDto>>> GetAllStocks(int id)
        {
            var stocks = await _stockService.GetAllStocksByStoreId(id);

            if (stocks is null)
                return NotFound(stocks);

            return Ok(stocks);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<GetStockResponseDto>> GetStock([FromRoute] int id)
        {
            var stock = await _stockService.GetStockByProductId(id);

            if (stock is null)
                return NotFound(stock);

            return Ok(stock);
        }

        [HttpPut("product/{id}")]
        public async Task<ActionResult<GetStockResponseDto>> UpdateStock([FromRoute] int id, UpdateStockRequest newStock)
        {
            var stock = await _stockService.UpdateStockByProductId(id, newStock);

            if (stock is null)
                return NotFound(stock);

            return Ok(stock);
        }
    }
}
