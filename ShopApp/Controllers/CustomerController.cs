using Microsoft.AspNetCore.Mvc;
using ShopApp.Dtos.Customer;
using ShopApp.Services.Generic;

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
