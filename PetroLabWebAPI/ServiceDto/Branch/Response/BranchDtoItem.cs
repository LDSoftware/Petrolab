namespace PetroLabWebAPI.ServiceDto.Branch.Response;

public record BranchDtoItem(long Id, string Code, string Name);

public record BranchDoctorDtoItem(long DoctorId, string Name);