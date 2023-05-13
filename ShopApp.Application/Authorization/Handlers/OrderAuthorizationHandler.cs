using Microsoft.AspNetCore.Authorization;
using ShopApp.Application.Authorization.Requirements;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Application.Authorization.Handlers
{
    public class OrderAuthorizationHandler : AuthorizationHandler<OrderRequirement, BaseOrder>
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderAuthorizationHandler(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OrderRequirement requirement,
            BaseOrder resource)
        {
            if (!int.TryParse(context.User.FindFirst("UserId")?.Value, out int userId))
            {
                return Task.CompletedTask;
            }

            var customer = GetCustomer(resource.OrderId);

            if (userId == customer.Result.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private async Task<GetCustomerResponseDto> GetCustomer(int orderId)
        {
            var order =  await _orderService.GetById(orderId); 
            return await _customerService.GetById(order.CustomerId);
        }
    }
}
