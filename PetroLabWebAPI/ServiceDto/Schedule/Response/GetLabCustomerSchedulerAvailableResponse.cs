using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Schedule.Response;

public class GetLabCustomerSchedulerDateTimeResponse(IList<ScheduleDateDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<ScheduleDateDtoItem>?>(_dataResult, _commonActionResponse);

public record ScheduleDateDtoItem(int Day, IList<ScheduleHourDtoItem> Hours);
public record ScheduleHourDtoItem(string Hour, bool IsReserved);
public record GetLabCustomerSchedulerDateTimeRequest(long IdBranch, long IdLabStudio, int Month);

