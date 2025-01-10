using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.LabStudio.Response;

public class GetLabStudioByBrachResponse(IList<GetLabStudioByBrachDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
    : GetCommonReponse<IList<GetLabStudioByBrachDtoItem>?>(_dataResult, _commonActionResponse);


public record GetLabStudioByBrachDtoItem(long Id, string Code, string Name);