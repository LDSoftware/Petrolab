using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Branch.Response;

public class GetBranchScheduleResponse(GetBranchScheduleDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<GetBranchScheduleDtoItem?>(_dataResult, _commonActionResponse);

public class GetAllBranchScheduleResponse(IList<GetBranchScheduleDtoItem>? _dataResult, 
CommonActionResponse _commonActionResponse) : GetCommonReponse<IList<GetBranchScheduleDtoItem>?>(_dataResult, _commonActionResponse);

public record GetBranchScheduleDtoItem(BranchDtoItem? Branch, 
    IList<BranchScheduleDtoItem> BranchSchedule, 
    IList<BranchScheduleTempDtoItem> BranchScheduleTemp, 
    IList<BranchScheduleDoctorDtoItem> BranchScheduleDoctor);

public record BranchScheduleDtoItem(long Id, long IdLabBranch, string DayOfWeek, string TimeInit, string TimeEnd);
public record BranchScheduleTempDtoItem(long Id, long IdLabBranch, DateTime Day, string TimeInit, string TimeEnd, long IdDoctor);
public record BranchScheduleDoctorDtoItem(long Id, long IdLabBranch, string TimeInit, string TimeEnd, long DoctorId);
