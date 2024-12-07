using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Doctor.Response;

public class GetDoctorResponse(IList<DoctorDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<DoctorDtoItem>?>(_dataResult, _commonActionResponse);
