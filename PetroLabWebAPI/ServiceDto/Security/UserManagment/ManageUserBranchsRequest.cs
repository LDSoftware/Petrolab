namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public record ManageUserBranchsRequest(string UserId, List<SelectedBrach> Branch);
public record DelteUserBranchsRequest(string UserId, List<long> Branch);
