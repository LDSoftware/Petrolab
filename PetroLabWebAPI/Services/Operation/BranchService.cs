using System.Data;
using System.Security.Permissions;
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
    IRepository<LabBranchDoctorMap> _repositoryMap
) : IBranchService
{
    private const string spName = "sp_AdminLabBranch";
    private const string spNameManageBranch = "sp_AdminLabBranchDoctorMap";
    public async Task<CreateActionResponse> CreateAsync(CreateBranchRequest request)
    {
        try
        {
            if (request.Doctors.Where(d => d.Equals(0)).Any())
            {
                return new(0, 400, "Bad Request - El id del doctor no puede ser 0");
            }

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

    public async Task<CommonActionResponse> DeleteDoctorToBranch(DeleteDoctorBranchRequest request)
    {
        try
        {
            string selectedDoctors = string.Join(",", request.Doctors);
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("IdLabBranch", request.BranchId, DbType.Int64);
            sp_parameters.Add("IdLabDoctors", selectedDoctors, DbType.String);
            var result = await _repository.Initialize(spNameManageBranch, sp_parameters).Delete();
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
                items.AddRange(result
                    .Select(r => new BranchDtoItem(r.Id, r.Code, r.Name, GetLabBranchDoctors(r.Id).Result)));
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
                item = new(result.Id, result.Code, result.Name, await GetLabBranchDoctors(result.Id));
            }
            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> InsertDoctorToBranch(CreateDoctorBranchRequest request)
    {
        try
        {
            if (request.Doctors.Where(d => d.Equals(0)).Any())
            {
                return new(400, "Bad Request - El id del doctor no puede ser 0");
            }

            string selectedDoctors = string.Join(",", request.Doctors);
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("IdLabBranch", request.BranchId, DbType.Int64);
            sp_parameters.Add("IdLabDoctors", selectedDoctors, DbType.String);
            var result = await _repository.Initialize(spNameManageBranch, sp_parameters).InsertOrUpdate();
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

    private async Task<List<BranchDoctorDtoItem>> GetLabBranchDoctors(long BranchId)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("IdLabBranch", BranchId, DbType.Int64);
            var result = await _repositoryMap.Initialize(spNameManageBranch, sp_parameters).Table();
            if (!result.Any())
            {
                throw new Exception();
            }
            return result.Select(r => new BranchDoctorDtoItem(r.IdLabDoctor, r.Doctor)).ToList();
        }
        catch (Exception)
        {
            return new();
        }
    }

}
