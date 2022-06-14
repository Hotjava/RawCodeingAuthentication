using Microsoft.AspNetCore.Authorization;

namespace RawCodingAuthentication.AuthorizationRequirement;

public class CustomRequiredClaims:IAuthorizationRequirement
{
    public CustomRequiredClaims(string claimType)
    {
        ClaimType = claimType;
    }

    public string ClaimType { get; set; }
}

public class CustomRequiredClaimHandler : AuthorizationHandler<CustomRequiredClaims>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequiredClaims requirement)
    {
        if (context.User.Claims.Any(p => p.Type == requirement.ClaimType))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
        
    }
}