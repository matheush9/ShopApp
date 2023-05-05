using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.DTOs.Order;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<GetOrderResponseDto, AddOrderRequestDto>
    {
        private readonly IOrderService _orderService;

        public OrderController(IGenericService<GetOrderResponseDto, AddOrderRequestDto> genericService, IOrderService orderService) : base(genericService)
        {
            _orderService = orderService;
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult> GetOrdersByCustomerId([FromRoute] int id)
        {
            var orders = await _orderService.GetOrdersByCustomerId(id);

            if (orders is null)
                return NotFound(orders);

            return Ok(orders);
        }
    }
}
