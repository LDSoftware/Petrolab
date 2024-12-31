using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.LabStudio.Request;
using PetroLabWebAPI.ServiceDto.LabStudio.Response;

namespace PetroLabWebAPI.Services;

public interface ILabStudioService
{
    Task<CreateActionResponse> CreateAsync(CreateLabStudioRequest request);
    Task<CommonActionResponse> UpdateAsync(UpdateLabStudioRequest request);
    Task<CommonActionResponse> DeleteAsync(DeleteLabStudioRequest request);
    Task<GetLabStudioResponse> GetLabStudioAsync();
    Task<GetLabStudioByIdResponse> GetLabStudioByIdAsync(long Id);
    Task<GetLabSpecialtyResponse> GetLabStudioBySpecialtyAsync();
}
