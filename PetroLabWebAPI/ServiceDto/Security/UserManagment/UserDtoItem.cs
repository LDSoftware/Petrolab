namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record UserDtoItem(string UserId,
string UserName,
string Email,
string Role,
string FistName,
string LastName,
string MotherLastName,
bool IsLocked,
List<UserBranchDtoItem>? Branchs);

public record UserBranchDtoItem(long Id, long BranchId, string BranchName, bool IsPrincipal);