namespace PetroLabWebAPI.ServiceDto.Doctor.Response;

public record DoctorDtoItem(long Id, string FirstName, string LastName, string MotherLastName, List<GetDoctorLabStudioDtoItem> LabStudios);
public record GetDoctorLabStudioDtoItem(long IdLabStudio, string Code, string Name);
