namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public record LabStudioDtoItem(long Id, string Code, int Type, string Name, int Duration, long Specialty);
public record GetLabStudioDtoItem(long Id, string Code, int Type, string Name, int Duration, long Specialty, string SpecialtyName);