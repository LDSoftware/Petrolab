using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.ServiceDto.Customer.Response;

public class GetCustomerByIdResponse(CustomerDtoItem? _dataResult, CommonActionResponse _commonActionResponse)
: GetCommonReponse<CustomerDtoItem?>(_dataResult, _commonActionResponse);
