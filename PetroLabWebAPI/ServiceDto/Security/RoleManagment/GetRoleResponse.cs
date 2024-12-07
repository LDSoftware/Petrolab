using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Security.RoleManagment;

public class GetRoleResponse(List<RoleDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<List<RoleDtoItem>?>(_dataResult, _commonActionResponse);

public record RoleDtoItem(string Id, string Name);