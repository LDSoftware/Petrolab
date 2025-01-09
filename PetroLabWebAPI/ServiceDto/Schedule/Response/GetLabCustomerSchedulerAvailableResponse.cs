using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Schedule.Response;

public class GetLabCustomerSchedulerDateTimeResponse(IList<ScheduleDateDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<ScheduleDateDtoItem>?>(_dataResult, _commonActionResponse);

public record ScheduleDateDtoItem(string Day, IList<ScheduleHourDtoItem> Hours);
public class ScheduleHourDtoItem(string _Hour, bool _IsReserved)
{
    public string Hour { get => _Hour; set => _Hour = value; }
    public bool IsReserved { get => _IsReserved; set => _IsReserved = value; }
}
public record GetLabCustomerSchedulerDateTimeRequest(long IdBranch, long IdLabStudio, DateTime startDate, DateTime endDate);

