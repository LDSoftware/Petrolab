using Peasy;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Doctor.Request;
using PetroLabWebAPI.Services;
using PetroLabWebAPI.ValidationRules.Rules;

namespace PetroLabWebAPI.ValidationRules.Commands;

public class InsertNewLabStudioCommand
(
    IDoctorService _doctorService,
    ValidateNoDuplicateLabStudioOnDoctor _validateNoDuplicateLabStudioOnDoctor
) : CommandBase<CommonActionResponse>
{

    public ManageDoctorLabStudioRequest Request { get; set; } = null!;

    protected override Task<IEnumerable<IRule>> OnGetRulesAsync()
    {
        List<IRule> rules = new();
        _validateNoDuplicateLabStudioOnDoctor.Request = Request;
        rules.Add(_validateNoDuplicateLabStudioOnDoctor);
        return TheseRules(rules.ToArray());
    }

    protected override async Task<CommonActionResponse> OnExecuteAsync()
    {
        return await _doctorService.InsertNewDoctorLabStudio(Request);
    }
}
