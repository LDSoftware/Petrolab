using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services.Operation;

public interface IBranchSheduleService
{
    Task<CreateActionResponse> CreateBranchWithSchedule(CreateBranchWithScheduleRequest request);
    Task<CommonActionResponse> AddScheduleAsync(CreateBranchScheduleRequest request);
    Task<CommonActionResponse> AddScheduleTempAsync(CreateScheduleTempRequest request);
    Task<CommonActionResponse> AddScheduleDoctorAsync(CreateScheduleDoctorRequest request);
    Task<CommonActionResponse> UpdateScheduleTempAsync(UpdateScheduleTempRequest request);
    Task<CommonActionResponse> UpdateScheduleDoctorAsync(UpdateScheduleDoctorRequest request);
    Task<CommonActionResponse> UpdateScheduleAsync(UpdateBranchScheduleRequest request);
    Task<CommonActionResponse> RemoveDoctorAsync(long Id);
    Task<CommonActionResponse> RemoveScheduleTempAsync(long Id);
    Task<GetBranchScheduleResponse> GetBranchScheduleAsync(long IdBranch);
}
