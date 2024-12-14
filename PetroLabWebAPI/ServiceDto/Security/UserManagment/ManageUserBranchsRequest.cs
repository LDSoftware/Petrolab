namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record ManageUserBranchsRequest(string UserId, List<SelectedBrach> Branch);

