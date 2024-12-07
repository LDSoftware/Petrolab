namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record LockUnlockUserRequest(string UserId, int Action);
