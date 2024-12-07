using System.Data;
using AutoMapper;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Customer.Request;
using PetroLabWebAPI.ServiceDto.Customer.Response;

namespace PetroLabWebAPI.Services;

public class CustomerService
(
    IRepository<Customer> _repository,
    IMapper _mapper
) : ICustomerService
{
    private const string spName = "sp_AdminLabCustomer";

    public async Task<CreateActionResponse> CreateAsync(CreateCustomerRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("FirstName", request.FirstName, DbType.String);
            sp_parameters.Add("LastName", request.LastName, DbType.String);
            sp_parameters.Add("MotherLastName", request.MotherLastName, DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).InsertOrUpdate();
            if (!result.Success)
            {
                throw new Exception(result.Message);
            }

            return new(result.ResultId);
        }
        catch (Exception ex)
        {
            return new(0, 500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> DeleteAsync(DeleteCustomerRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).Delete();
            if (!result.Success)
            {
                throw new Exception(result.Message);
            }
            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<GetCustomerResponse> GetCustomerAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).Table();
            List<CustomerDtoItem> items = new();
            {
                items.AddRange(_mapper.Map<List<CustomerDtoItem>>(result));
            }
            return new(items, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetCustomerByIdResponse> GetCustomerByIdAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEI", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).GetById();
            CustomerDtoItem item = null!;
            if (result is not null)
            {
                item = _mapper.Map<CustomerDtoItem>(result);
            }
            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateAsync(UpdateCustomerRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("FirstName", request.FirstName, DbType.String);
            sp_parameters.Add("LastName", request.LastName, DbType.String);
            sp_parameters.Add("MotherLastName", request.MotherLastName, DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).InsertOrUpdate();
            if (!result.Success)
            {
                throw new Exception(result.Message);
            }
            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }
}
