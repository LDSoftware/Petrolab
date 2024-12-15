namespace PetroLabWebAPI.ServiceDto.Branch.Request;

public record CreateDoctorBranchRequest(long BranchId, List<long> Doctors);
public record DeleteDoctorBranchRequest(long BranchId, List<long> Doctors);