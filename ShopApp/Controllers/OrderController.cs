using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Domain.DTOs.Order;

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
