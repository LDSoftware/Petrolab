namespace PetroLabWebAPI.Data.Domain;

public record LabBranchScheduleDoctor(long Id, long IdLabBranch, string TimeInit, string TimeEnd, long DoctorId);

