using System;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Customer.Request;
using PetroLabWebAPI.ServiceDto.Customer.Response;

namespace PetroLabWebAPI.Services;

public interface ICustomerService
{
    Task<CreateActionResponse> CreateAsync(CreateCustomerRequest request);
    Task<CommonActionResponse> UpdateAsync(UpdateCustomerRequest request);
    Task<CommonActionResponse> DeleteAsync(DeleteCustomerRequest request);
    Task<GetCustomerResponse> GetCustomerAsync();
    Task<GetCustomerByIdResponse> GetCustomerByIdAsync(long Id);
}
