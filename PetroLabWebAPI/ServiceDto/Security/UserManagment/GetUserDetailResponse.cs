using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Security.UserManagment;

public class GetUserDetailResponse(UserDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<UserDtoItem?>(_dataResult, _commonActionResponse);
