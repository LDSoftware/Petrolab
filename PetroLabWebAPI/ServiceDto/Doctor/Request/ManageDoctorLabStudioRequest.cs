namespace PetroLabWebAPI.ServiceDto.Doctor.Request;

public record ManageDoctorLabStudioRequest(long DoctorId, List<long> LabStudios);
