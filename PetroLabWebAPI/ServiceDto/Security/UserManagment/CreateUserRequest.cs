namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record CreateUserRequest(string UserName, 
string Email, 
string Password, 
string Role,
string FistName,
string LastName,
string MotherLastName,
List<SelectedBrach> Branch);


public record SelectedBrach(long BranchId, bool IsPrincipal);