namespace PetroLabWebAPI.Data.Domain;

public record LabBranchScheduleDoctor(long Id, long IdLabBranch, long DoctorId, string TimeInit, string TimeEnd, string DoctorName);

