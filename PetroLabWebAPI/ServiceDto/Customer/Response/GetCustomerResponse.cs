using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Customer.Response;

public class GetCustomerResponse(IList<CustomerDtoItem>? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<IList<CustomerDtoItem>?>(_dataResult, _commonActionResponse);
