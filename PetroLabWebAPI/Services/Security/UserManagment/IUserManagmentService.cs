using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;

namespace PetroLabWebAPI.Services.Security.UserManagment;

public interface IUserManagmentService
{
    Task<CommonActionCreateUser> CreateUser(CreateUserRequest request);
    Task<CommonActionResponse> UpdateUser(UpdateUserRequest request);
    Task<CommonActionResponse> DeleteUser(string UserId);
    Task<CommonActionResponse> LockUnlockUser(LockUnlockUserRequest request);
    Task<GetUserDetailResponse> GetUsersById(string UserId);
    Task<GetUsersResponse> GetUsers();
    Task<List<UserBranchDtoItem>> GetBranchByUser(string UserId);
    Task<CommonActionResponse> InsertNewBranchOnUser(ManageUserBranchsRequest request);
    Task<CommonActionResponse> DeleteBranchOnUser(ManageUserBranchsRequest request);
}
