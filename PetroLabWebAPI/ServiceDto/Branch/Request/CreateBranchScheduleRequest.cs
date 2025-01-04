namespace PetroLabWebAPI.ServiceDto.Branch.Request;


public record CreateBranchScheduleRequest(long IdLabBranch, IList<CreateScheduleItemDto> Schedule,
IList<CreateScheduleTempItemDto> ScheduleTemp, IList<CreateScheduleDoctorItemDto> Doctors);

public record CreateScheduleItemDto(string DayOfWeek, string TimeInit, string TimeEnd);
public record CreateScheduleTempItemDto(DateTime Day, string TimeInit, string TimeEnd);
public record CreateScheduleDoctorItemDto(long DoctorId, string TimeInit, string TimeEnd);

public record CreateScheduleIRequest(long IdLabBranch, string DayOfWeek, string TimeInit, string TimeEnd);
public record CreateScheduleTempRequest(long IdLabBranch, DateTime Day, string TimeInit, string TimeEnd);
public record CreateScheduleDoctorRequest(long IdLabBranch, long DoctorId, string TimeInit, string TimeEnd);