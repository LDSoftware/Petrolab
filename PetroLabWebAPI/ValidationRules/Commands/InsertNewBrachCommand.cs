using Peasy;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;
using PetroLabWebAPI.Services.Security.UserManagment;
using PetroLabWebAPI.ValidationRules.Rules;

namespace PetroLabWebAPI.ValidationRules.Commands;

public class InsertNewBrachCommand
(
    IUserManagmentService _userManagmentService,
    ValidateNoDuplicateBranchOnUser _validateNoDuplicateBranchOnUser
) : CommandBase<CommonActionResponse>
{

    private ManageUserBranchsRequest _request = null!;

    public ManageUserBranchsRequest Request
    {
        set => _request = value;
        get => _request;
    }

    protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
    {
        List<IRule> rules = new();
        _validateNoDuplicateBranchOnUser.Request = _request;
        rules.Add(_validateNoDuplicateBranchOnUser);
        return TheseRules(rules.ToArray());
    }

    protected override Task<CommonActionResponse> OnExecuteAsync()
    {
        return _userManagmentService.InsertNewBranchOnUser(_request);
    }
}
