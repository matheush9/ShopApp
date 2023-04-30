using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Domain.DTOs.Store;

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
