using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Doctor.Request;
using PetroLabWebAPI.ServiceDto.Doctor.Response;

namespace PetroLabWebAPI.Services;

public interface IDoctorService
{
    Task<CreateActionResponse> CreateAsync(CreateDoctorRequest request);
    Task<CommonActionResponse> UpdateAsync(UpdateDoctorRequest request);
    Task<CommonActionResponse> DeleteAsync(DeleteDoctorRequest request);
    Task<GetDoctorResponse> GetDoctorAsync(long IdBranch);
    Task<GetDoctorByIdResponse> GetDoctorByIdAsync(long Id);
}
