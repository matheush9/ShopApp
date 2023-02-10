using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Order;
using ShopApp.Services.GenericService;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : GenericController<GetOrderResponseDto, AddOrderRequestDto>
    {
        public OrderController(IGenericService<GetOrderResponseDto, AddOrderRequestDto> genericService) : base(genericService)
        {

        }
    }
}
