using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Store;
using ShopApp.Services.StoreServices;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<GetStoreResponseDto>>> GetStores()
        {
            var stores = await _storeService.GetAllStores();

            if (stores.Success is false)
                return NotFound(stores);

            return Ok(stores);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetStoreResponseDto>>> GetStore([FromRoute] int id)
        {
            var store = await _storeService.GetStoreById(id);

            if (store.Success is false)
                return NotFound(store);

            return Ok(store);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetStoreResponseDto>>> AddStore([FromBody] AddStoreRequestDto newStore)
        {
            var addedStore = await _storeService.AddStore(newStore);

            if (addedStore.Success is false)
                return NotFound(addedStore);

            return Ok(addedStore);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetStoreResponseDto>>> DeleteStore([FromRoute] int id)
        {
            var store = await _storeService.DeleteStore(id);

            if (store.Success is false)
                return NotFound(store);

            return Ok(store);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<GetStoreResponseDto>>> UpdateStore([FromRoute] int id, AddStoreRequestDto newStore)
        {
            var store = await _storeService.UpdateStore(id, newStore);

            if (store.Success is false)
                return NotFound(store);

            return Ok(store);
        }
    }
}
