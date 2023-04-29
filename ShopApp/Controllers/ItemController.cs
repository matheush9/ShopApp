using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Item;
using ShopApp.Services.Generic;

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
