namespace PetroLabWebAPI.Security.Claims;

public interface IIdentityClaimService
{
    string GetUserNameClaim(HttpContext httpContext);
    string GetRoleNameClaim(HttpContext httpContext);
}
