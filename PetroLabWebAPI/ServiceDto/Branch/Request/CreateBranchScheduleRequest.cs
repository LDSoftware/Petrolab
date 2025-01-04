namespace PetroLabWebAPI.ServiceDto.Branch.Request;


public record CreateBranchWithScheduleRequest(CreateBranchRequest BranchRequest, CreateBranchScheduleRequest ScheduleRequest);

public record CreateBranchScheduleRequest(long IdLabBranch, IList<CreateScheduleItemDto> Schedule,
IList<CreateScheduleTempItemDto> ScheduleTemp, IList<CreateScheduleDoctorItemDto> Doctors);

public record CreateScheduleItemDto(string DayOfWeek, string TimeInit, string TimeEnd);
public record CreateScheduleTempItemDto(DateTime Day, string TimeInit, string TimeEnd);
public record CreateScheduleDoctorItemDto(long DoctorId, string TimeInit, string TimeEnd);

public record CreateScheduleIRequest(long IdLabBranch, IList<CreateScheduleItemDto> Schedule);
public record CreateScheduleTempRequest(long IdLabBranch, IList<CreateScheduleTempItemDto> ScheduleTemp);
public record CreateScheduleDoctorRequest(long IdLabBranch, IList<CreateScheduleDoctorItemDto> Doctors);