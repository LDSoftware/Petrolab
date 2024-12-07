namespace PetroLabWebAPI.ServiceDto.Doctor.Response;

public record DoctorDtoItem(long Id, string FirstName, string LastName, string MotherLastName, List<GetLabStudioDtoItem> LabStudios);
public record GetLabStudioDtoItem(long IdLabStudio, string Code, string Name);
