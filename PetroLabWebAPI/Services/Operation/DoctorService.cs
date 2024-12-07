using System.Data;
using AutoMapper;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Doctor.Request;
using PetroLabWebAPI.ServiceDto.Doctor.Response;

namespace PetroLabWebAPI.Services;

public class DoctorService
(
    IRepository<Doctor> _repository,
    IMapper _mapper
) : IDoctorService
{
    private const string spName = "sp_AdminLabDoctor";

    public async Task<CreateActionResponse> CreateAsync(CreateDoctorRequest request)
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

    public async Task<CommonActionResponse> DeleteAsync(DeleteDoctorRequest request)
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

    public async Task<GetDoctorResponse> GetDoctorAsync(long IdBranch)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            if (IdBranch.Equals(0))
            {
                sp_parameters.Add("Action", "SEA", DbType.String);
            }
            else
            {
                sp_parameters.Add("Action", "SEL", DbType.String);
                sp_parameters.Add("BranchSelectFilter", IdBranch, DbType.Int64);
            }
            var result = await _repository.Initialize(spName, sp_parameters).Table();
            List<DoctorDtoItem> items = new();
            {
                items.AddRange(_mapper.Map<List<DoctorDtoItem>>(result));
            }
            return new(items, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetDoctorByIdResponse> GetDoctorByIdAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEI", DbType.String);
            var result = await _repository.Initialize(spName, sp_parameters).GetById();
            DoctorDtoItem item = null!;
            if (result is not null)
            {
                item = _mapper.Map<DoctorDtoItem>(result);
            }
            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateAsync(UpdateDoctorRequest request)
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
