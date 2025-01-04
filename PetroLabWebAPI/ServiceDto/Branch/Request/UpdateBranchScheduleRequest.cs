namespace PetroLabWebAPI.ServiceDto.Branch.Request;

public record UpdateBranchScheduleRequest(long IdLabBranch, IList<UpdateScheduleItemDto> Schedule,
IList<UpdateScheduleTempItemDto> ScheduleTemp, IList<UpdateScheduleDoctorItemDto> Doctors);

public record UpdateScheduleItemDto(long IdLabBranch, long Id, string DayOfWeek, string TimeInit, string TimeEnd);
public record UpdateScheduleTempItemDto(long IdLabBranch, long Id, DateTime Day, string TimeInit, string TimeEnd);
public record UpdateScheduleDoctorItemDto(long IdLabBranch, long Id, long DoctorId, string TimeInit, string TimeEnd);


public record UpdateScheduleRequest(long IdLabBranch, long Id, string DayOfWeek, string TimeInit, string TimeEnd);
public record UpdateScheduleTempRequest(long IdLabBranch, long Id, DateTime Day, string TimeInit, string TimeEnd);
public record UpdateScheduleDoctorRequest(long IdLabBranch, long Id, long DoctorId, string TimeInit, string TimeEnd);
