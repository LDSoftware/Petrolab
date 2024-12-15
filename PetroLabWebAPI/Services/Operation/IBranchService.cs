using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services;

public interface IBranchService
{
    Task<CreateActionResponse> CreateAsync(CreateBranchRequest request);
    Task<CommonActionResponse> UpdateAsync(UpdateBranchRequest request);
    Task<CommonActionResponse> DeleteAsync(DeleteBranchRequest request);
    Task<GetBranchResponse> GetBranchAsync();
    Task<GetBranchByIdResponse> GetBranchByIdAsync(long Id);
    Task<CommonActionResponse> InsertDoctorToBranch(CreateDoctorBranchRequest request);
    Task<CommonActionResponse> DeleteDoctorToBranch(DeleteDoctorBranchRequest request);
}
