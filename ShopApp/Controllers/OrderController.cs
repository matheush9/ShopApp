using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.DTOs.Order;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);

            if (order is null)
                return NotFound(order);

            return Ok(order);
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult> GetOrdersByCustomerId([FromRoute] int id)
        {
            var orders = await _orderService.GetOrdersByCustomerId(id);

            if (orders is null)
                return NotFound(orders);

            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddOrderRequestDto newOrder)
        {
            var orderResponse = await _orderService.Add(newOrder);
            return Ok(orderResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var order = await _orderService.Delete(id);

            if (order is null)
                return NotFound(order);

            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, AddOrderRequestDto newOrder)
        {
            var order = await _orderService.Update(id, newOrder);

            if (order is null)
                return NotFound(order);

            return Ok(order);
        }
    }
}
