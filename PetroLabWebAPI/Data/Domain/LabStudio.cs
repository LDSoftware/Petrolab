namespace PetroLabWebAPI.Data.Domain;

public record LabStudio(long Id, string Code, int Type, string Name, int Duration, long Specialty);
public record GetLabStudio(long Id, string Code, int Type, string Name, int Duration, long Specialty, string SpecialtyName);
