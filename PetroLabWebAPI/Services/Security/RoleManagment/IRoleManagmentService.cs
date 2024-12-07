using System;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Security.RoleManagment;

namespace PetroLabWebAPI.Services.Security.RoleManagment;

public interface IRoleManagmentService
{
    Task<CommonActionResponse> CreateRole(CreateRoleRequest request);
    Task<CommonActionResponse> UpdateRole(UpdateRoleRequest request);
    Task<GetRoleResponse> GetRoles();
    Task<CommonActionResponse> DeleteRole(DeleteRoleRequest request);
}
