namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record UpdateUserRequest(string UserId,
string UserName, 
string Email, 
string Password, 
string Role,
string FistName,
string LastName,
string MotherLastName,
long Branch);
