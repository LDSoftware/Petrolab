using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Branch.Response;

public class GetBranchByIdResponse(BranchDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<BranchDtoItem?>(_dataResult, _commonActionResponse);

