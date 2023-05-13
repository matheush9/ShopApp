using Microsoft.AspNetCore.Authorization;
using ShopApp.Application.Authorization.Requirements;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Domain.Common;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Application.Authorization.Handlers
{
    public class StoreAuthorizationHandler : AuthorizationHandler<StoreRequirement, BaseStore>
    {
        private readonly IStoreService _storeService;

        public StoreAuthorizationHandler(IStoreService storeService)
        {
            _storeService = storeService;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            StoreRequirement requirement,
            BaseStore resource)
        {
            if (!int.TryParse(context.User.FindFirst("UserId")?.Value, out int userId))
            {
                return Task.CompletedTask;
            }

            var customer = GetStore(resource.StoreId);

            if (userId == customer.Result.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private async Task<GetStoreResponseDto> GetStore(int storeId)
        {
            return await _storeService.GetById(storeId);
        }
    }
}
