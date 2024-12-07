using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Security.Login;

public class LoginResponse(LoginDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<LoginDtoItem?>(_dataResult, _commonActionResponse);

public record LoginDtoItem(string UserId, string FirstName, string LastName, string MotherLastName, string Token);
