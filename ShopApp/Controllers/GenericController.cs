using Microsoft.AspNetCore.Mvc;
using ShopApp.Services.GenericService;

namespace ShopApp.Controllers
{
    public class GenericController<T, T2>: Controller where T : class
    {
        private IGenericService<T, T2> _genericService;

        public GenericController(IGenericService<T, T2> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public Task<ServiceResponse<List<T>>> GetAll()
        {
            return _genericService.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ServiceResponse<T>> GetById(int id)
        {
            return _genericService.GetById(id);
        }

        [HttpPost]
        public Task<ServiceResponse<T>> Add(T2 requestEntity)
        {
            return _genericService.Add(requestEntity);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ServiceResponse<T>> Update(int id, T2 requestEntity)
        {
            return _genericService.Update(id, requestEntity);
        }

        [HttpDelete]
        public Task<ServiceResponse<T>> Delete(int id)
        {
            return _genericService.Delete(id);
        }
    }
}
