using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public class GetUsersResponse(List<UserDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<List<UserDtoItem>?>(_dataResult, _commonActionResponse);
