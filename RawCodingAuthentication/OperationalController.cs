using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace RawCodingAuthentication;

public class OperationalController:Controller
{
    private readonly IAuthorizationService _authorizationService;

    public OperationalController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }
    
    public async Task<IActionResult> Open()
    {
        return View();
    }

}

public class CookeJarAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        OperationAuthorizationRequirement requirement)
    {
        if (requirement.Name == CookieJarOperations.Open)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }
        }
        return Task.CompletedTask;
    }
}

public static class CookieJarOperations
{
    public static string Open = "Open";
    public static string Close = "Close";
    public static string Lock = "Lock";
}