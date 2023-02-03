using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Store;
using ShopApp.Services.GenericService;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : GenericController<GetStoreResponseDto, AddStoreRequestDto>
    {
        public StoreController(IGenericService<GetStoreResponseDto, AddStoreRequestDto> genericService) : base(genericService)
        {

        }
    }
}
