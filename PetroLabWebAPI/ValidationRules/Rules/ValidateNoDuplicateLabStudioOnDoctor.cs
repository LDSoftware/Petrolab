using Peasy;
using PetroLabWebAPI.ServiceDto.Doctor.Request;
using PetroLabWebAPI.Services;

namespace PetroLabWebAPI.ValidationRules.Rules;

public class ValidateNoDuplicateLabStudioOnDoctor
(
    IDoctorService _doctorService
) : RuleBase
{
    private const string spName = "sp_AdminLabDoctorStudioMap";

    public ManageDoctorLabStudioRequest Request { get; set; } = null!;

    protected override async Task OnValidateAsync()
    {
        var labStudios = await _doctorService.GetLabStudiosByDoctor(Request.DoctorId);
        List<long> duplicates = labStudios.Select(e => e.IdLabStudio).Intersect(Request.LabStudios).ToList();
        if (duplicates.Any())
        {
            Invalidate($"No se puedeb duplicar los estudios, verifique!.");
        }
    }
}
