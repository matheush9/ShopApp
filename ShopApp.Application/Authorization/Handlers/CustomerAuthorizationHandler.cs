using Microsoft.AspNetCore.Authorization;
using ShopApp.Application.Authorization.Requirements;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Application.Authorization.Handlers
{
    public class CustomerAuthorizationHandler : AuthorizationHandler<CustomerRequirement, BaseCustomer>
    {
        private readonly ICustomerService _customerService;

        public CustomerAuthorizationHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomerRequirement requirement,
            BaseCustomer resource)
        {
            if (!int.TryParse(context.User.FindFirst("UserId")?.Value, out int userId))
            {
                return Task.CompletedTask;
            }

            var customer = GetCustomer(resource.CustomerId);

            if (userId == customer.Result.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private async Task<GetCustomerResponseDto> GetCustomer(int customerId)
        {
            return await _customerService.GetById(customerId);
        }
    }
}
