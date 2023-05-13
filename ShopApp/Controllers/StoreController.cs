using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IAuthorizationService _authorizationService;

        public StoreController(IStoreService storeService, IAuthorizationService authorizationService)
        {
            _storeService = storeService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> GetById([FromRoute] int id)
        {
            var store = await _storeService.GetById(id);

            if (store is null)
                return NotFound(store);

            return Ok(store);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> GetStoreByUserId([FromRoute] int id)
        {
            var store = await _storeService.GetStoreByUserId(id);

            if (store is null)
                return NotFound(store);

            return Ok(store);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GetStoreResponseDto>> Add([FromBody] AddStoreRequestDto newStore)
        {
            if (await Authorize(newStore) is false)
                return Forbid();

            await _storeService.Add(newStore);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> Delete([FromRoute] int id)
        {
            var getStore = await _storeService.GetById(id);

            if (await Authorize(getStore) is false)
                return Forbid();

            var store = await _storeService.Delete(id);

            if (store is null)
                return NotFound(store);

            return Ok(store);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetStoreResponseDto>> Update([FromRoute] int id, AddStoreRequestDto newStore)
        {
            var getStore = await _storeService.GetById(id);

            if (await Authorize(getStore) is false)
                return Forbid();

            var store = await _storeService.Update(id, newStore);

            if (store is null)
                return NotFound(store);

            return Ok(store);
        }

        [NonAction]
        public async Task<bool> Authorize(BaseUser baseUser)
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseUser, "UserPolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
