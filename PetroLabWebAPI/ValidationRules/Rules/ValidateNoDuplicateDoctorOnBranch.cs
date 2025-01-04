using Peasy;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.Services;

namespace PetroLabWebAPI.ValidationRules.Rules;

public class ValidateNoDuplicateDoctorOnBranch
(
    IBranchService _branchService
) : RuleBase
{
    public CreateDoctorBranchRequest Request { get; set; } = null!;

    protected async override Task OnValidateAsync()
    {
        var doctors = await _branchService.GetBranchByIdAsync(Request.BranchId);


        var haveRequestDoctorIdZero = (Request.Doctors.Where(r => r.Equals(0))).Any();

        if (haveRequestDoctorIdZero)
        {
            Invalidate($"No se puede guardar sucursales con Id 0, verifique!.");
        }

    }
}
