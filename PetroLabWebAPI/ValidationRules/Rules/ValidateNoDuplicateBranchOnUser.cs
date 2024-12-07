using Peasy;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;
using PetroLabWebAPI.Services.Security.UserManagment;

namespace PetroLabWebAPI.ValidationRules.Rules;

public class ValidateNoDuplicateBranchOnUser
(
    IUserManagmentService _userManagmentService
) : RuleBase
{
    private const string spName = "sp_AdminUserBranchMap";

    public ManageUserBranchsRequest Request { get; set; } = null!;

    protected override async Task OnValidateAsync()
    {
        var branches = await _userManagmentService.GetBranchByUser(Request.UserId);
        List<long> duplicates = branches.Select(e => e.BranchId).Intersect(Request.Branch).ToList();
        if (duplicates.Any())
        {
            Invalidate($"No se puedeb duplicar las sucursales, verifique!.");
        }
    }
}
