namespace PetroLabWebAPI.ServiceDto.Customer.Request;

public record UpdateCustomerRequest(long Id, string FirstName, string LastName, string MotherLastName);
