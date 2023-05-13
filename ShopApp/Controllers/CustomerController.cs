using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthorizationService _authorizationService;

        public CustomerController(ICustomerService customerService, IAuthorizationService authorizationService)
        {
            _customerService = customerService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerResponseDto>> GetById([FromRoute] int id)
        {
            var customer = await _customerService.GetById(id);

            if (customer is null)
                return NotFound(customer);

            return Ok(customer);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<GetCustomerResponseDto>> GetCustomerByUserId([FromRoute] int id)
        {
            var customer = await _customerService.GetCustomerByUserId(id);

            if (customer is null)
                return NotFound(customer);

            return Ok(customer);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GetCustomerResponseDto>> Add([FromBody] AddCustomerRequestDto newCustomer)
        {
            if (await Authorize(newCustomer) is false)
                return Forbid();

            await _customerService.Add(newCustomer);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetCustomerResponseDto>> Delete([FromRoute] int id)
        {            
            var getCustomer = await _customerService.GetById(id);

            if (await Authorize(getCustomer) is false)
                return Forbid();

            var customer = await _customerService.Delete(id);

            if (customer is null)
                return NotFound(customer);

            return Ok(customer);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<GetCustomerResponseDto>> Update([FromRoute] int id, AddCustomerRequestDto newCustomer)
        {
            var getCustomer = await _customerService.GetById(id);
            
            if (await Authorize(getCustomer) is false) 
                return Forbid();

            var customer = await _customerService.Update(id, newCustomer);

            if (customer is null)
                return NotFound(customer);

            return Ok(customer);
        }

        [NonAction]
        public async Task<bool> Authorize(BaseUser baseUser) 
        {
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, baseUser, "UserPolicy");

            if (!authorizationResult.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
