using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Branch.Response;

public class GetBranchResponse(IList<BranchDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<IList<BranchDtoItem>?>(_dataResult, _commonActionResponse);
