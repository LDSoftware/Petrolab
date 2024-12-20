using Microsoft.AspNetCore.Identity;
using PetroLabWebAPI.Security.Entity;
using PetroLabWebAPI.ServiceDto.Security.Login;
using PetroLabWebAPI.Services.Security.Jwt;
using PetroLabWebAPI.Services.Security.RoleManagment;

namespace PetroLabWebAPI.Services.Security.Login;

public class UserLoginService
(
    SignInManager<User> _signInManager,
    UserManager<User> _userManager,
    ISecurityTokenService _securityTokenService,
    IRoleManagmentService _roleManagmentService
) : IUserLoginService
{

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        try
        {
            var identityUser = await _userManager.FindByEmailAsync(request.UserName);
            if (identityUser == null)
            {
                return new(null, new(500, "Usuario no encontrado."));
            }
            else
            {
                var userClaims = await _userManager.GetClaimsAsync(identityUser!);
                var role = await _userManager.GetRolesAsync(identityUser!);

                var result = await _signInManager.PasswordSignInAsync(identityUser!, request.Password, false, false);
                var userRole = await _roleManagmentService.GetRoles();
                string IdRole = string.Empty;

                if (userRole.ServiceStatus.Code.Equals(200))
                {
                    var existRole = userRole.DataResult!.FirstOrDefault(e => e.Name.Equals(role.First()));
                    if (existRole == null)
                    {
                        IdRole = "0";
                    }
                    else
                    {
                        IdRole = userRole.DataResult!.FirstOrDefault(e => e.Name.Equals(role.First()))?.Id!;
                    }
                }
                if (result.Succeeded)
                {
                    LoginDtoItem loginInfo = new(
                        identityUser.Id,
                        identityUser.FirstName,
                        identityUser.LastName,
                        identityUser.MotherLastName,
                        _securityTokenService.CreateUserToken(identityUser!, role.First()));

                    return new(loginInfo, new());
                }
                else if (result.IsLockedOut)
                {
                    return new(null, new(403, "Forbidden"));
                }
                else if (result.IsNotAllowed)
                {
                    return new(null, new(401, "Unauthorized"));
                }
                else
                {
                    return new(null, new(400, "Bad Request"));
                }
            }
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }

    }
}

