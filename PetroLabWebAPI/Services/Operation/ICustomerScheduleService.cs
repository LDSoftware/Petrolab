using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Schedule.Request;
using PetroLabWebAPI.ServiceDto.Schedule.Response;

namespace PetroLabWebAPI.Services.Operation;

public interface ICustomerScheduleService
{
    Task<CommonActionResponse> CreateCustomerScheduleAsync(CreateCustomerScheduleRequest request);
    Task<CommonActionResponse> UpdateCustomerScheduleAsync(UpdateCustomerScheduleRequest request);
    Task<CommonActionResponse> CancelCustomerScheduleAsync(CancelCustomerScheduleRequest request);
    Task<GetLabCustomerScheduleResponse> GetLabCustomerScheduleResponseAsync(LabCustomerScheduleFilterRequest filterRequest);
}
