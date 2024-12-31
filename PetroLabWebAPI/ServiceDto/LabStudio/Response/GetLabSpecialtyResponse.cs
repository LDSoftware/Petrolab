using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabSpecialtyResponse(IList<LabSpecialtyDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<LabSpecialtyDtoItem>?>(_dataResult, _commonActionResponse);
