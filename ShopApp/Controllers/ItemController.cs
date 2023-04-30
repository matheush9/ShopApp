using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Domain.DTOs.Item;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : GenericController<GetItemResponseDto, AddItemRequestDto>
    {
        public ItemController(IGenericService<GetItemResponseDto, AddItemRequestDto> genericService) : base(genericService)
        {

        }
    }
}
