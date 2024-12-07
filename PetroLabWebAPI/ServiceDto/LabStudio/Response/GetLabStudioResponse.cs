using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabStudioResponse(IList<LabStudioDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<LabStudioDtoItem>?>(_dataResult, _commonActionResponse);
