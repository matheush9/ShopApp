using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Stock;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IAuthorizationService _authorizationService;

        public StockController(IStockService stockService, IAuthorizationService authorizationService)
        {
            _stockService = stockService;
            _authorizationService = authorizationService;
            
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
            var getStock = await _stockService.GetStock(id);

            if (await Authorize(getStock) is false)
                return Forbid();

            var stock = await _stockService.UpdateStockByProductId(id, newStock);

            if (stock is null)
                return NotFound(stock);

            return Ok(stock);
        }

        [NonAction]
        public async Task<bool> Authorize(BaseStore baseStore)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseStore, "StorePolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
