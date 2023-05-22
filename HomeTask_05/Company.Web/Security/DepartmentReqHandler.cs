using Microsoft.AspNetCore.Authorization;

namespace Company.Web.Security;

public class DepartmentReqHandler : AuthorizationHandler<DepartmentReq>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DepartmentReq requirement)
    {
        var dep = context.User.Claims.Where(c => c.Type == "Department").FirstOrDefault();
        if (dep?.Value == requirement.Department)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
