namespace PetroLabWebAPI.ServiceDto.Branch.Response;

public record BranchDtoItem(long Id, string Code, string Name, List<BranchDoctorDtoItem> Doctors);

public record BranchDoctorDtoItem(long DoctorId, string Name);