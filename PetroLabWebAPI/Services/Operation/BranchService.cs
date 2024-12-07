using System.Data;
using AutoMapper;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services;

public class BranchService
(
    IRepository<Branch> _repository,
    IMapper _mapper    
) : IBranchService
{
    private const string spName = "sp_AdminLabBranch";
    public async Task<CreateActionResponse> CreateAsync(CreateBranchRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("Code", request.Code, DbType.String);
            sp_parameters.Add("Name", request.Name, DbType.String);
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

    public async Task<CommonActionResponse> DeleteAsync(DeleteBranchRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
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

    public async Task<GetBranchResponse> GetBranchAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).Table();
            List<BranchDtoItem> items = new();
            if (result.Any())
            {
                items.AddRange(_mapper.Map<List<BranchDtoItem>>(result));
            }
            return new(items, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetBranchByIdResponse> GetBranchByIdAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEI", DbType.String);
            sp_parameters.Add("Id", Id, DbType.Int64);
            var result = await _repository.Initialize(spName, sp_parameters).GetById();
            BranchDtoItem item = null!;
            if (result is not null)
            {
                item = _mapper.Map<BranchDtoItem>(result);
            }
            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateAsync(UpdateBranchRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("Code", request.Code, DbType.String);
            sp_parameters.Add("Name", request.Name, DbType.String);
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
