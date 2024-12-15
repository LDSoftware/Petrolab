namespace PetroLabWebAPI.ServiceDto.Branch.Request;

public record CreateBranchRequest(string Code, string Name, List<long> Doctors);
