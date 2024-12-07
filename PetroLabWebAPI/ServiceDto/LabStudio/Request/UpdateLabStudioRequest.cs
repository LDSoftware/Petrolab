namespace PetroLabWebAPI.ServiceDto.LabStudio.Request;

public record UpdateLabStudioRequest(long Id, string Code, int Type, string Name, int Duration);
