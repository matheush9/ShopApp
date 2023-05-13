﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Item;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IAuthorizationService _authorizationService;

        public ItemController(IItemService itemService, IAuthorizationService authorizationService)
        {
            _itemService = itemService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> GetById([FromRoute] int id)
        {
            var item = await _itemService.GetById(id);

            if (item is null)
                return NotFound(item);

            return Ok(item);
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult> GetItemsByOrderId([FromRoute] int id)
        {
            var items = await _itemService.GetItemsByOrderId(id);

            if (items is null)
                return NotFound(items);

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<GetStoreResponseDto>> Add([FromBody] AddItemRequestDto newItem)
        {
            if (await Authorize(newItem) is false)
                return Forbid();

            await _itemService.Add(newItem);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> Delete([FromRoute] int id)
        {
            var getItem = await _itemService.GetById(id);

            if (await Authorize(getItem) is false)
                return Forbid();

            var item = await _itemService.Delete(id);

            if (item is null)
                return NotFound(item);

            return Ok(item);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> Update([FromRoute] int id, AddItemRequestDto newItem)
        {
            var getItem = await _itemService.GetById(id);

            if (await Authorize(getItem) is false)
                return Forbid();

            var item = await _itemService.Update(id, newItem);

            if (item is null)
                return NotFound(item);

            return Ok(item);
        }

        [NonAction]
        public async Task<bool> Authorize(BaseOrder baseOrder)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseOrder, "OrderPolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
