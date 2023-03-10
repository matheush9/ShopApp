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
        [Route("{id}")]
        public async Task<ActionResult<T>> GetById([FromRoute] int id)
        {
            var entity = await _genericService.GetById(id);

            if (entity is null)
                return NotFound(entity);

            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<T>> Add([FromBody] T2 newEntity)
        {
            await _genericService.Add(newEntity);
            return Ok();
        }

        [HttpDelete]        
        [Route("{id}")]
        public async Task<ActionResult<T>> Delete([FromRoute] int id)
        {
            var entity = await _genericService.Delete(id);

            if (entity is null)
                return NotFound(entity);

            return Ok(entity);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<T>> Update([FromRoute] int id, T2 newEntity)
        {
            var entity = await _genericService.Update(id, newEntity);

            if (entity is null)
                return NotFound(entity);

            return Ok(entity);
        }
    }
}
