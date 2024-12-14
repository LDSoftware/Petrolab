using Peasy;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;
using PetroLabWebAPI.Services.Security.UserManagment;

namespace PetroLabWebAPI.ValidationRules.Rules;

public class ValidateNoDuplicateBranchOnUser
(
    IUserManagmentService _userManagmentService
) : RuleBase
{
    public ManageUserBranchsRequest Request { get; set; } = null!;

    protected override async Task OnValidateAsync()
    {
        var branches = await _userManagmentService.GetBranchByUser(Request.UserId);
        List<long> duplicates = branches.Select(e => e.BranchId)
            .Intersect(Request.Branch.Select(r => r.BranchId)).ToList();

        var havePrincipalInSaved = (branches.Where(d => d.IsPrincipal.Equals(true))).Any();
        var havePrincipalInRequest = (Request.Branch.Where(r => r.IsPrincipal.Equals(true))).Any();
        var haveRequestBranchIdZero = (Request.Branch.Where(r => r.BranchId.Equals(0))).Any();
        
        if (haveRequestBranchIdZero)
        {
            Invalidate($"No se puede guardar sucursales con Id 0, verifique!.");
        }

        if (havePrincipalInSaved && havePrincipalInRequest)
        {
            Invalidate($"No se puede tener 2 sucursales como principal, verifique!.");
        }

        if (duplicates.Any())
        {
            Invalidate($"No se puede duplicar las sucursales, verifique!.");
        }
    }
}
