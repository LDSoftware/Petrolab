namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public record LabSpecialityGamasDtoItem(long Id, string Name, IList<LabSpecialityGamaDtoItem> LabStudios);

public record LabSpecialityGamaDtoItem(long Id, string Name, string Code, int Type, long SpecialityId, string SpecialityName);
