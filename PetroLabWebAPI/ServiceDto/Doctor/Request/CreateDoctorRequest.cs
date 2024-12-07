namespace PetroLabWebAPI.ServiceDto.Doctor.Request;

public record CreateDoctorRequest(string FirstName, string LastName, string MotherLastName, List<long> LabStudio);