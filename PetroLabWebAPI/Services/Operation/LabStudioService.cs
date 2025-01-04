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
    IRepository<LabSpecialty> _specialtyRepository,
    IRepository<GetLabStudioDtoItem> _getLabStudioRepository,
    IRepository<LabSpecialityGamas> _specialityGamasRepository,
    IMapper _mapper
) : ILabStudioService
{
    private const string spName = "sp_AdminLabStudio";
    private const string spSpecialtyName = "sp_GetLabSpecialty";
    private const string spGetLabSpecialityGamas = "sp_GetLabSpecialityGamas";

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
            sp_parameters.Add("Speciality", request.Speciality, DbType.Int64);
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

    public async Task<GetLabSpecialityGamasResponse> GetLabSpecialityGamasAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new();
            var groups = await _specialtyRepository.Initialize(spSpecialtyName, sp_parameters).Table();
            var result = await _specialityGamasRepository.Initialize(spGetLabSpecialityGamas, sp_parameters).Table();
            List<LabSpecialityGamasDtoItem> items = new();

            if (result is not null && result.Any())
            {
                foreach (var speciality in groups)
                {                    
                    var labStudios = result.Where(s => s.SpecialityId.Equals(speciality.Id))
                        .Select(r => new { r.Id, r.Code, r.Type, r.Name, r.SpecialityName });                        
                    items.Add(new LabSpecialityGamasDtoItem(speciality.Id,speciality.Name, labStudios
                    .Select(x => new LabSpecialityGamaDtoItem(x.Id, x.Name, x.Code, x.Type, speciality.Id, x.SpecialityName))
                    .ToList()));
                }
            }

            return new(items, new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<GetLabStudioResponse> GetLabStudioAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "SEL", DbType.String);
            var result = await _getLabStudioRepository.Initialize(spName, sp_parameters).Table();
            List<GetLabStudioDtoItem> items = new();
            {
                items.AddRange(result.Select(x => new GetLabStudioDtoItem(x.Id, x.Code, x.Type, x.Name, x.Duration, x.Specialty, x.SpecialtyName)));
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
            sp_parameters.Add("Id", Id, DbType.String);
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

    public async Task<GetLabSpecialtyResponse> GetLabStudioBySpecialtyAsync()
    {
        try
        {
            DynamicParameters sp_parameters = new();
            var result = await _specialtyRepository.Initialize(spSpecialtyName, sp_parameters).Table();
            List<LabSpecialtyDtoItem> items = new();
            if (result is not null && result.Any())
            {
                items.AddRange(result.Select(x => new LabSpecialtyDtoItem(x.Id, x.Name)));
            }
            return new(items, new());
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
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("Code", request.Code, DbType.String);
            sp_parameters.Add("Type", request.Type, DbType.Int32);
            sp_parameters.Add("Name", request.Name, DbType.String);
            sp_parameters.Add("Duration", request.Duration, DbType.Int32);
            sp_parameters.Add("Speciality", request.Speciality, DbType.Int64);
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
