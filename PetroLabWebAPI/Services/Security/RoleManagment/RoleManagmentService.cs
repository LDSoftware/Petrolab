using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetroLabWebAPI.Security.Claims;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Security.RoleManagment;

namespace PetroLabWebAPI.Services.Security.RoleManagment;

public class RoleManagmentService
(
    RoleManager<IdentityRole> _roleManager,
    IIdentityClaimService _identityClaimService,
    IHttpContextAccessor _httpContextAccessor
) : IRoleManagmentService
{
    public async Task<CommonActionResponse> CreateRole(CreateRoleRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                if (!(await _roleManager.RoleExistsAsync(request.Name)))
                {
                    var newRole = new IdentityRole(request.Name);
                    var created = await _roleManager.CreateAsync(newRole);
                    if (created.Succeeded)
                    {
                        return new();
                    }
                    else
                    {
                        throw new Exception(String.Join(",", created.Errors.Select(e => e.Description).ToArray()));
                    }
                }
                else
                {
                    throw new Exception("Nombre de role duplicado");
                }
            }
            else
            {
                throw new Exception("Se requiere nombre del role");
            }
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> DeleteRole(DeleteRoleRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            var roles = await _roleManager.Roles.Where(r => r.Id!.Equals(request.Id)).ToListAsync();
            if (roles.Any())
            {
                var deleted = await _roleManager.DeleteAsync(roles.First());
                if (!deleted.Succeeded)
                {
                    throw new Exception(String.Join(",", deleted.Errors.Select(t => t.Description)));
                }

                return new();
            }
            else
            {
                throw new Exception("Rol no encontrado");
            }
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<GetRoleResponse> GetRoles()
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(null, new(403, "Forbidden"));
            }

            var roles = await _roleManager.Roles.ToListAsync();
            return new(roles.Select(y => new RoleDtoItem(y.Id, y.Name!)).ToList(), new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateRole(UpdateRoleRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            var roles = await _roleManager.Roles.Where(r => r.Id!.Equals(request.Id)).ToListAsync();
            if (roles.Any())
            {
                roles.First().Name = request.Name;
            }
            var updated = await _roleManager.UpdateAsync(roles.First());
            if (!updated.Succeeded)
            {
                throw new Exception(String.Join(",", updated.Errors.Select(t => t.Description)));
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }
}
