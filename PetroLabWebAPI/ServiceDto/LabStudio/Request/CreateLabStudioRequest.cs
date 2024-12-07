namespace PetroLabWebAPI.ServiceDto.LabStudio.Request;

public record CreateLabStudioRequest(string Code, int Type, string Name, int Duration);
