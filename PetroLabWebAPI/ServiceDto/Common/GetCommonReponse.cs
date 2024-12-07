namespace PetroLabWebAPI.ServiceDto.Common;

public class GetCommonReponse<Dto>(Dto? _dataResult, CommonActionResponse _commonActionResponse)
{
    public Dto? DataResult => _dataResult;
    public CommonActionResponse ServiceStatus => _commonActionResponse;
}
