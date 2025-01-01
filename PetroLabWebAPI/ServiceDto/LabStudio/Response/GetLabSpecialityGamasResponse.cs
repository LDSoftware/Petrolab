using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabSpecialityGamasResponse(IList<LabSpecialityGamasDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<LabSpecialityGamasDtoItem>?>(_dataResult, _commonActionResponse);

