namespace PetroLabWebAPI.Data.Domain;

public record LabBranchScheduleTemp(long Id, long IdLabBranch, DateTime Day, string TimeInit, string TimeEnd, long IdDoctor);
