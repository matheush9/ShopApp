﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Order;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private IAuthorizationService _authorizationService;

        public OrderController(IOrderService orderService, IAuthorizationService authorizationService)
        {
            _orderService = orderService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderResponseDto>> GetById(int id)
        {
            var order = await _orderService.GetById(id);

            if (order is null)
                return NotFound(order);

            return Ok(order);
        }

        [HttpGet("customer/{id}")]
        public async Task<ActionResult<List<GetOrderResponseDto>>> GetOrdersByCustomerId([FromRoute] int id)
        {
            var orders = await _orderService.GetOrdersByCustomerId(id);

            if (orders is null)
                return NotFound(orders);

            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<GetOrderResponseDto>> Add([FromBody] AddOrderRequestDto newOrder)
        {
            if (await Authorize(newOrder) is false)
                return Forbid();

            var orderResponse = await _orderService.Add(newOrder);
            return Ok(orderResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var getOrder = await _orderService.GetById(id);

            if (getOrder is null)
                return NotFound(getOrder);

            if (await Authorize(getOrder) is false)
                return Forbid();

            var order = await _orderService.Delete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GetOrderResponseDto>> Update([FromRoute] int id, AddOrderRequestDto newOrder)
        {
            var getOrder = await _orderService.GetById(id);

            if (getOrder is null)
                return NotFound(getOrder);

            if (await Authorize(getOrder) is false)
                return Forbid();

            var order = await _orderService.Update(id, newOrder);

            return Ok(order);
        }

        [NonAction]
        public async Task<bool> Authorize(BaseCustomer baseCustomer)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseCustomer, "CustomerPolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
