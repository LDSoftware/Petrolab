using System.Data;
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
    IRepository<LabStudioDoctorMap> _labStudioDoctorMapRepository
) : IDoctorService
{
    private const string spName = "sp_AdminLabDoctor";
    private const string spNameManageLabStudio = "sp_AdminLabDoctorStudioMap";

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

            var insertLabStudio = await InsertNewDoctorLabStudio(new(result.ResultId, request.LabStudio));
            if (!insertLabStudio.Code.Equals(200))
            {
                throw new Exception(insertLabStudio.Message);
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

    public async Task<CommonActionResponse> DeleteDoctorLabStudio(ManageDoctorLabStudioRequest request)
    {
        try
        {
            string selectedLabStudio = string.Join(",", request.LabStudios);
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("IdLabDoctor", request.DoctorId, DbType.Int64);
            sp_parameters.Add("SelectedLabStudios", selectedLabStudio, DbType.String);
            var result = await _labStudioDoctorMapRepository.Initialize(spNameManageLabStudio, sp_parameters).InsertOrUpdate();
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
                items.AddRange(result.Select(e => new DoctorDtoItem(e.Id, e.FirstName,
                e.LastName, e.MotherLastName, GetLabStudiosByDoctor(e.Id).Result)));
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
            sp_parameters.Add("Id", Id, DbType.Int64);
            var result = await _repository.Initialize(spName, sp_parameters).GetById();
            DoctorDtoItem item = null!;
            if (result is not null)
            {
                item = new(result.Id, result.FirstName,
                result.LastName, result.MotherLastName,
                await GetLabStudiosByDoctor(Id));
            }

            return new(item, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<List<GetLabStudioDtoItem>> GetLabStudiosByDoctor(long IdDoctor)
    {
        try
        {
            List<GetLabStudioDtoItem> response = new();
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            sp_parameters.Add("IdLabDoctor", IdDoctor, DbType.Int64);
            var result = await _labStudioDoctorMapRepository.Initialize(spNameManageLabStudio, sp_parameters).Table();
            if (result.Any())
            {
                response.AddRange(result.Select(r => new GetLabStudioDtoItem(r.IdLabStudio, r.Code, r.Name)));
            }
            return response;
        }
        catch
        {
            return new();
        }
    }

    public async Task<CommonActionResponse> InsertNewDoctorLabStudio(ManageDoctorLabStudioRequest request)
    {
        try
        {
            string selectedLabStudio = string.Join(",", request.LabStudios);
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("IdLabDoctor", request.DoctorId, DbType.Int64);
            sp_parameters.Add("SelectedLabStudios", selectedLabStudio, DbType.String);
            var result = await _labStudioDoctorMapRepository.Initialize(spNameManageLabStudio, sp_parameters).InsertOrUpdate();
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
