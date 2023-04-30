using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : GenericController<GetCustomerResponseDto, AddCustomerRequestDto>
    {
        public CustomerController(IGenericService<GetCustomerResponseDto, AddCustomerRequestDto> genericService) : base(genericService)
        {

        }
    }
}
