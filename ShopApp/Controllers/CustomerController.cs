using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : GenericController<GetCustomerResponseDto, AddCustomerRequestDto>
    {
        private readonly ICustomerService _customerService;

        public CustomerController(IGenericService<GetCustomerResponseDto, AddCustomerRequestDto> genericService, ICustomerService customerService) : base(genericService)
        {
            _customerService = customerService;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetCustomerByUserId([FromRoute] int id)
        {
            var customer = await _customerService.GetCustomerByUserId(id);

            if (customer is null)
                return NotFound(customer);

            return Ok(customer);
        }
    }
}
