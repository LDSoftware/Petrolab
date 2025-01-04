using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services.Operation;

public interface IBranchSheduleService
{
    Task<CommonActionResponse> AddScheduleAsync(CreateBranchScheduleRequest request);
    Task<CommonActionResponse> AddScheduleTempAsync(CreateScheduleTempRequest request);
    Task<CommonActionResponse> UpdateScheduleTempAsync(UpdateScheduleTempRequest request);
    Task<CommonActionResponse> RemoveScheduleTempAsync(long Id);
    Task<CommonActionResponse> AddScheduleDoctorAsync(CreateScheduleDoctorRequest request);
    Task<CommonActionResponse> UpdateScheduleDoctorAsync(UpdateScheduleDoctorRequest request);
    Task<CommonActionResponse> RemoveDoctorAsync(long Id);
}
