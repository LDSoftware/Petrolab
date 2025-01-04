namespace PetroLabWebAPI.ServiceDto.Branch.Request;

public record UpdateBranchScheduleRequest(long IdLabBranch, IList<UpdateScheduleItemDto> Schedule,
IList<UpdateScheduleTempItemDto> ScheduleTemp, IList<UpdateScheduleDoctorItemDto> Doctors);

public record UpdateScheduleItemDto(long Id, string DayOfWeek, string TimeInit, string TimeEnd);
public record UpdateScheduleTempItemDto(long Id, DateTime Day, string TimeInit, string TimeEnd);
public record UpdateScheduleDoctorItemDto(long Id, long DoctorId, string TimeInit, string TimeEnd);


public record UpdateScheduleRequest(long IdLabBranch, IList<UpdateScheduleItemDto> Schedule);
public record UpdateScheduleTempRequest(long IdLabBranch, IList<UpdateScheduleTempItemDto> ScheduleTemp);
public record UpdateScheduleDoctorRequest(long IdLabBranch, IList<UpdateScheduleDoctorItemDto> Doctors);
