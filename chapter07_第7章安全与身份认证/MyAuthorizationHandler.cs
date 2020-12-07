using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter07_Samples
{
    public class MyAuthorizationHandler : AuthorizationHandler<MyAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MyAuthorizationRequirement requirement)
        {
            if (context.User.Identity.Name?.StartsWith("admin") == true)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
