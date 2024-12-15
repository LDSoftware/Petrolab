using Peasy;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.Services;
using PetroLabWebAPI.ValidationRules.Rules;

namespace PetroLabWebAPI.ValidationRules.Commands;

public class InsertNewDoctorCommand
(
    IBranchService _branchService,
    ValidateNoDuplicateDoctorOnBranch _validateNoDuplicateDoctorOnBranch
) : CommandBase<CommonActionResponse>
{
    public CreateDoctorBranchRequest Request { get; set; } = null!;

    protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
    {
        List<IRule> rules = new();
        _validateNoDuplicateDoctorOnBranch.Request = Request;
        rules.Add(_validateNoDuplicateDoctorOnBranch);
        return TheseRules(rules.ToArray());        
    }

    protected override Task<CommonActionResponse> OnExecuteAsync()
    {
        return _branchService.InsertDoctorToBranch(Request);
    }
}
