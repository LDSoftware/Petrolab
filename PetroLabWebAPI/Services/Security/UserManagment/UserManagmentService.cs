using System.Data;
using System.Security.Claims;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.Security.Claims;
using PetroLabWebAPI.Security.Entity;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;

namespace PetroLabWebAPI.Services.Security.UserManagment;

public class UserManagmentService
(
    UserManager<User> _userManager,
    RoleManager<IdentityRole> _roleManager,
    IIdentityClaimService _identityClaimService,
    IHttpContextAccessor _httpContextAccessor,
    IRepository<BranchUserMap> _branchUserMapRepository,
    IRepository<BranchUserMapView> _branchUserMapViewRepository
) : IUserManagmentService
{

    private const string spName = "sp_AdminUserBranchMap";

    public async Task<CommonActionCreateUser> CreateUser(CreateUserRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(Code: 403, Message: "Forbidden");
            }

            if (string.IsNullOrEmpty(request.FistName) ||
                string.IsNullOrEmpty(request.LastName) ||
                string.IsNullOrEmpty(request.MotherLastName) ||
                request.Branch.Equals(0))
            {
                throw new Exception("Bad Request");
            }

            if (request.Branch.Where(r => r.IsPrincipal.Equals(true)).Count() > 1)
            {
                return new(Code: 400, Message: "Bad Request - Solo se puede tener una sucursal como principal");
            }

            var existRole = _roleManager.Roles.Where(r => r.Name!.Equals(request.Role));
            if (!existRole.Any())
            {
                return new(Code: 400, Message: $"Bad Request - El Role {request.Role} no existe!");
            }

            var passwordResult = _userManager.PasswordHasher.HashPassword(
                new User()
                {
                    Email = request.Email,
                    UserName = request.Email,
                    NormalizedUserName = request.UserName,
                    PasswordHash = request.Password
                }, request.Password);

            var identityUser = new User()
            {
                Email = request.Email,
                UserName = request.Email,
                NormalizedUserName = request.UserName,
                PasswordHash = passwordResult,
                LockoutEnabled = false,
                FirstName = request.FistName,
                LastName = request.LastName,
                MotherLastName = request.MotherLastName
            };
            var identityResult = await _userManager.CreateAsync(identityUser);
            var claim = new Claim("UserType", request.Role);
            var result = await _userManager.AddClaimAsync(identityUser, claim);
            var role = await _roleManager.FindByNameAsync(request.Role);
            var assignedRole = await _userManager.AddToRoleAsync(identityUser, role?.Name!);

            string selectedBranchs = string.Join(",", request.Branch.Select(e => e.BranchId));
            var principalBranch = request.Branch.Where(p => p.IsPrincipal.Equals(true));

            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("UserId", identityUser.Id, DbType.String);
            sp_parameters.Add("SelectedBranch", selectedBranchs, DbType.String);
            if (principalBranch != null && principalBranch.Any())
            {
                sp_parameters.Add("BranchIdPrincipal", principalBranch.First().BranchId, DbType.Int64);
            }
            var resultBranch = await _branchUserMapRepository.Initialize(spName, sp_parameters).InsertOrUpdate();
            if (!resultBranch.Success)
            {
                throw new Exception(resultBranch.Message);
            }

            if (identityResult.Succeeded && assignedRole.Succeeded)
            {
                return new(identityUser.Id);
            }
            else
            {
                var msgErr = identityResult.Errors.Count() > 0 ? identityResult.Errors.First().Description : "Bad Request";
                return new(Code: 500, Message: msgErr);
            }
        }
        catch (Exception ex)
        {
            return new(Code: 500, Message: ex.Message);
        }
    }

    public async Task<CommonActionResponse> DeleteUser(string UserId)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                throw new Exception("No se encuentra el usuario");
            }
            var deleteResult = await _userManager.DeleteAsync(user);

            if (deleteResult.Succeeded)
            {
                return new();
            }
            else
            {
                return new(400, "Bad Request");
            }
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<GetUsersResponse> GetUsers()
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(null, new(403, "Forbidden"));
            }

            List<UserDtoItem> results = new();
            var usersData = await _userManager.Users.ToListAsync();
            if (usersData.Any())
            {
                results.AddRange(usersData.Select(u => new UserDtoItem
                    (
                        u.Id,
                        u.UserName!,
                        u.Email!,
                        _userManager.GetRolesAsync(u).Result.First(),
                        u.FirstName,
                        u.LastName,
                        u.MotherLastName,
                        u.LockoutEnd.HasValue,
                        GetBranchByUser(u.Id).Result)));
            }

            return new(results, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetUserDetailResponse> GetUsersById(string UserId)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(null, new(403, "Forbidden"));
            }

            UserDtoItem user = null!;

            var results = await _userManager.Users.FirstOrDefaultAsync(e => e.Id.Equals(UserId));
            if (results != null)
            {
                user = new(
                    results.Id,
                    results.UserName!,
                    results.Email!,
                    _userManager.GetRolesAsync(results).Result.First(),
                    results.FirstName,
                    results.LastName,
                    results.MotherLastName,
                    results.LockoutEnd.HasValue, await GetBranchByUser(UserId));
            }
            return new(user, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> LockUnlockUser(LockUnlockUserRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            if (request.Action == 1)
            {
                var lockoutEndDate = new DateTime(2999, 01, 01);
                await _userManager.SetLockoutEnabledAsync(user!, true);
                await _userManager.SetLockoutEndDateAsync(user!, lockoutEndDate);
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(user!, false);
                user!.LockoutEnd = null;
                user.LockoutEnabled = false;
                var updateResult = await _userManager.UpdateAsync(user);
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> UpdateUser(UpdateUserRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(403, "Forbidden");
            }

            var user = await _userManager.FindByIdAsync(request.UserId);
            user!.UserName = request.Email;
            user.FirstName = request.FistName;
            user.LastName = request.LastName;
            user.MotherLastName = request.MotherLastName;

            // Si viene en blanco el password no lo actualizamos
            if (!string.IsNullOrEmpty(request.Password))
            {
                var updatePasswordResult = _userManager.PasswordHasher.HashPassword(
              user, request.Password);
                user.PasswordHash = updatePasswordResult;
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded)
            {
                return new();
            }
            else
            {
                return new(400, "Bad Request");
            }
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<List<UserBranchDtoItem>> GetBranchByUser(string UserId)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            sp_parameters.Add("UserId", UserId, DbType.String);
            var resultBranch = await _branchUserMapViewRepository.Initialize(spName, sp_parameters).Table();
            List<UserBranchDtoItem> branchs = new();
            if (resultBranch.Any())
            {
                branchs.AddRange(resultBranch.Select(b => new UserBranchDtoItem(b.Id,
                b.BranchId, b.BranchName, b.IsPrincipal)));
            }
            return branchs;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new();
        }
    }

    public async Task<CommonActionResponse> InsertNewBranchOnUser(ManageUserBranchsRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(Code: 403, Message: "Forbidden");
            }

            string selectedBranchs = string.Join(",", request.Branch.Select(e => e.BranchId));
            var principalBranch = request.Branch.Where(p => p.IsPrincipal.Equals(true));

            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("UserId", request.UserId, DbType.String);
            sp_parameters.Add("SelectedBranch", selectedBranchs, DbType.String);
            if (principalBranch != null && principalBranch.Any())
            {
                sp_parameters.Add("BranchIdPrincipal", principalBranch.First().BranchId, DbType.Int64);
            }
            var resultBranch = await _branchUserMapRepository.Initialize(spName, sp_parameters).InsertOrUpdate();
            if (!resultBranch.Success)
            {
                throw new Exception(resultBranch.Message);
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> DeleteBranchOnUser(DelteUserBranchsRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(Code: 403, Message: "Forbidden");
            }

            string selectedBranchs = string.Join(",", request.Branch);
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("UserId", request.UserId, DbType.String);
            sp_parameters.Add("SelectedBranch", selectedBranchs, DbType.String);
            var resultBranch = await _branchUserMapRepository.Initialize(spName, sp_parameters).Delete();
            if (!resultBranch.Success)
            {
                throw new Exception(resultBranch.Message);
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> SetBranchToPrincipal(UpdateUserBranchToPrincipalRequest request)
    {
        try
        {
            if (!_identityClaimService.GetRoleNameClaim(_httpContextAccessor.HttpContext!).Equals("Administrator"))
            {
                return new(Code: 403, Message: "Forbidden");
            }

            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("UserId", request.UserId, DbType.String);
            sp_parameters.Add("BranchIdPrincipal", request.BranchId, DbType.String);
            var resultBranch = await _branchUserMapRepository.Initialize(spName, sp_parameters).InsertOrUpdate();
            if (!resultBranch.Success)
            {
                throw new Exception(resultBranch.Message);
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }
}
