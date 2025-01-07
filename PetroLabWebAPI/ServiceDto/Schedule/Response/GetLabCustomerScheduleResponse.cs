using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Schedule.Response;

public class GetLabCustomerScheduleResponse(IList<LabCustomerScheduleDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<LabCustomerScheduleDtoItem>?>(_dataResult, _commonActionResponse);

public record LabCustomerScheduleDtoItem(
    long Id, string Name, string LastName, string MotherLastName, 
    string CellPhone, string Phone, string Email, DateTime StarDate,
    DateTime EndDate, LabCustomerScheduleStudioDtoItem? LabStudio, 
    CustomerScheduleBranchDtoItem? IdBranch, string? Comments,
    string? ProofOfPayment, bool Cancel, DateTime? CancelDate);

public record LabCustomerScheduleFilterRequest( DateTime StarDate,
    DateTime EndDate, long IdLabStudio, long IdBranch, bool Cancel);

public record LabCustomerScheduleStudioDtoItem(long Id, string Code, int Type, string Name);

public record CustomerScheduleBranchDtoItem(long Id, string Code, string Name);

