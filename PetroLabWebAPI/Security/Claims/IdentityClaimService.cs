using System.Security.Claims;

namespace PetroLabWebAPI.Security.Claims;

public class IdentityClaimService : IIdentityClaimService
{
    public string GetUserNameClaim(HttpContext httpContext)
    {
        var identity = httpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claim = identity!.Claims;
        var usernameClaim = claim
            .Where(x => x.Type == ClaimTypes.Email)
            .FirstOrDefault();

        return usernameClaim!.Value;
    }

    public string GetRoleNameClaim(HttpContext httpContext)
    {
        var identity = httpContext.User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claim = identity!.Claims;
        var usernameClaim = claim
            .Where(x => x.Type == "UserType")
            .FirstOrDefault();

        return usernameClaim!.Value;
    }
}