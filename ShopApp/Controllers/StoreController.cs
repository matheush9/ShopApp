using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : GenericController<GetStoreResponseDto, AddStoreRequestDto>
    {
        private readonly IStoreService _storeService;

        public StoreController(IGenericService<GetStoreResponseDto, AddStoreRequestDto> genericService, IStoreService storeService) : base(genericService)
        {
            _storeService = storeService;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetStoreByUserId([FromRoute] int id)
        {
            var store = await _storeService.GetStoreByUserId(id);

            if (store is null)
                return NotFound(store);

            return Ok(store);
        }
    }
}
