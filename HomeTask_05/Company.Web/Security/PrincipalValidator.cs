using Company.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

internal class PrincipalValidator
{
    public static async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        var userId = context.Principal?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            context.RejectPrincipal();
            return;
        }

        if (!Guid.TryParse(userId, out var id))
        {
            context.RejectPrincipal();
            return;
        }

        // Get Unit Of Work using DI
        var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();

        var user = unitOfWork.UserRepo.Find(x => x.Id == id);
        if (user == null)
        {
            context.RejectPrincipal();
            return;
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FullName),
            new(ClaimTypes.Role, user.Role.Name),
            new("Department", user.Department.Name),
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie"));
        context.ReplacePrincipal(principal);
        context.ShouldRenew = true;
        await Task.CompletedTask;
    }
}