namespace PetroLabWebAPI.Data.Domain;

public record LabCustomerSchedule(
    long Id, string Name, string LastName, string MotherLastName, 
    string CellPhone, string Phone, string Email, DateTime StarDate,
    DateTime EndDate, long IdLabStudio, long IdBranch, string? Comments,
    string? ProofOfPayment, bool Cancel, DateTime? CancelDate);
