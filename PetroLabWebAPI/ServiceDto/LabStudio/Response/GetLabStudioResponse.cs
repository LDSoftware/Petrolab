using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabStudioResponse(IList<GetLabStudioDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<GetLabStudioDtoItem>?>(_dataResult, _commonActionResponse);
