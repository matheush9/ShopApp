using Microsoft.AspNetCore.Authorization;
using ShopApp.Application.Authorization.Requirements;
using ShopApp.Domain.Common;

namespace ShopApp.Application.Authorization.Handlers
{
    public class UserAuthorizationHandler: AuthorizationHandler<UserRequirement, BaseUser>
    {      
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            UserRequirement requirement,
            BaseUser resource)
        {
            if (!int.TryParse(context.User.FindFirst("UserId")?.Value, out int userId))
            {
                return Task.CompletedTask;
            }

            if (userId == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
