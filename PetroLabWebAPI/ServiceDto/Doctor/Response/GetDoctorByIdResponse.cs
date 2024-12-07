using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Doctor.Response;

public class GetDoctorByIdResponse(DoctorDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<DoctorDtoItem?>(_dataResult, _commonActionResponse);
