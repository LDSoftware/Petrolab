using System.Data;
using AutoMapper;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.LabStudio.Request;
using PetroLabWebAPI.ServiceDto.LabStudio.Response;

namespace PetroLabWebAPI.Services;

public class LabStudioService
(
    IRepository<LabStudio> _repository,
    IMapper _mapper
) : ILabStudioService
{
    private const string spName = "sp_AdminLabStudio";

    public async Task<CreateActionResponse> CreateAsync(CreateLabStudioRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("Code", request.Code, DbType.String);
            sp_parameters.Add("Type", request.Type, DbType.Int32);
            sp_parameters.Add("Name", request.Name, DbType.String);
            sp_parameters.Add("Duration", request.Duration, DbType.Int32);
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

    public async Task<CommonActionResponse> DeleteAsync(DeleteLabStudioRequest request)
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

    public async Task<GetLabStudioResponse> GetLabStudioAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).Table();
            List<LabStudioDtoItem> items = new();
            {
                items.AddRange(_mapper.Map<List<LabStudioDtoItem>>(result));
            }
            return new(items, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetLabStudioByIdResponse> GetLabStudioByIdAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEI", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).GetById();
            LabStudioDtoItem item = null!;
            if (result is not null)
            {
                item = _mapper.Map<LabStudioDtoItem>(result);
            }
            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateAsync(UpdateLabStudioRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("Code", request.Code, DbType.String);
            sp_parameters.Add("Type", request.Type, DbType.Int32);
            sp_parameters.Add("Name", request.Name, DbType.String);
            sp_parameters.Add("Duration", request.Duration, DbType.Int32);
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
