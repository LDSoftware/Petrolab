namespace PetroLabWebAPI.Data.Domain;

public record LabBranchSchedule(long Id, long IdLabBranch, string DayOfWeek, string TimeInit, string TimeEnd);
