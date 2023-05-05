using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.Item;
using ShopApp.Domain.DTOs.Item;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : GenericController<GetItemResponseDto, AddItemRequestDto>
    {
        private readonly IItemService _itemService;

        public ItemController(IGenericService<GetItemResponseDto, AddItemRequestDto> genericService, IItemService itemService) : base(genericService)
        {
            _itemService = itemService;
        }

        [HttpGet("order/{id}")]
        public async Task<ActionResult> GetItemsByOrderId([FromRoute] int id)
        {
            var items = await _itemService.GetItemsByOrderId(id);

            if (items is null)
                return NotFound(items);

            return Ok(items);
        }
    }
}
