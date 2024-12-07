namespace PetroLabWebAPI.ServiceDto.Doctor.Request;

public record UpdateDoctorRequest(long Id, string FirstName, string LastName, string MotherLastName);
