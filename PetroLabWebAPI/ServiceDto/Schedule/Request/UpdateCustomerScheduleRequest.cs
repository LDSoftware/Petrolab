namespace PetroLabWebAPI.ServiceDto.Schedule.Request;

public record UpdateCustomerScheduleRequest(
    long Id, string Name, string LastName, string MotherLastName, 
    string CellPhone, string Phone, string Email, DateTime StarDate,
    DateTime EndDate, long IdLabStudio, long IdBranch, string? Comments,
    string? ProofOfPayment);
