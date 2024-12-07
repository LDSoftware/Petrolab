using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabStudioByIdResponse(LabStudioDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<LabStudioDtoItem?>(_dataResult, _commonActionResponse);
