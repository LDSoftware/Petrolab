namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record CreateUserRequest(string UserName, 
string Email, 
string Password, 
string Role,
string FistName,
string LastName,
string MotherLastName,
List<long> Branch);
