using Microsoft.AspNetCore.Mvc;
using ShopApp.Services.GenericService;

namespace ShopApp.Controllers
{
    public class GenericController<T, T2>: Controller where T : class
    {
        private readonly IGenericService<T, T2> _genericService;

        public GenericController(IGenericService<T, T2> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<T>>>> GetAll()
        {
            var entityList = await _genericService.GetAll();

            if (entityList.Success is false)
                return NotFound(entityList);

            return Ok(entityList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<T>>> GetById([FromRoute] int id)
        {
            var entity = await _genericService.GetById(id);

            if (entity.Success is false)
                return NotFound(entity);

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<T>>> Add([FromBody] T2 newEntity)
        {
            var addedEntity = await _genericService.Add(newEntity);

            if (addedEntity.Success is false)
                return NotFound(addedEntity);

            return Ok(addedEntity);
        }

        [HttpDelete]        
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<T>>> Delete([FromRoute] int id)
        {
            var entity = await _genericService.Delete(id);

            if (entity.Success is false)
                return NotFound(entity);

            return Ok(entity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<ServiceResponse<T>>> Update([FromRoute] int id, T2 newEntity)
        {
            var entity = await _genericService.Update(id, newEntity);

            if (entity.Success is false)
                return NotFound(entity);

            return Ok(entity);
        }
    }
}
