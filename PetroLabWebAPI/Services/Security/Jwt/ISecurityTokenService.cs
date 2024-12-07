using Microsoft.AspNetCore.Identity;

namespace PetroLabWebAPI.Services.Security.Jwt;

public interface ISecurityTokenService
{
    string CreateUserToken(IdentityUser identityUser, string roleName);
}
