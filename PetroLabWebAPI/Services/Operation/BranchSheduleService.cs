using System.Data;
using Dapper;
using PetroLabWebAPI.Data.ExecutionModel;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services.Operation;

public class BranchSheduleService
(
    StoredProcRepository _storedProcRepository
) : IBranchSheduleService
{
    private const string _spAdminLabSchedule = "sp_AdminLabSchedule";
    private const string _spAdminLabScheduleTemp = "sp_AdminLabScheduleTemp";
    private const string _spAdminLabScheduleDoctor = "sp_AdminLabScheduleDoctor";

    public async Task<CommonActionResponse> AddScheduleDoctorAsync(CreateScheduleDoctorRequest request)
    {
        try
        {

            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
            sp_parameters.Add("DoctorId", request.DoctorId, DbType.Int64);
            sp_parameters.Add("TimeInit", request.TimeInit, DbType.String);
            sp_parameters.Add("TimeEnd", request.TimeEnd, DbType.String);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleDoctor, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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

    public async Task<CommonActionResponse> AddScheduleAsync(CreateBranchScheduleRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
            foreach (var item in request.Schedule)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "INS", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("DayOfWeek", item.DayOfWeek, DbType.String);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabSchedule, sp_parameters)
                .Execute<CommonExecutionModel>();
            }

            foreach (var item in request.ScheduleTemp)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "INS", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("Day", item.Day, DbType.DateTime);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabScheduleTemp, sp_parameters)
                .Execute<CommonExecutionModel>();
            }

            foreach (var item in request.Doctors)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "INS", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("DoctorId", item.DoctorId, DbType.Int64);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabScheduleDoctor, sp_parameters)
                .Execute<CommonExecutionModel>();
            }

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> AddScheduleTempAsync(CreateScheduleTempRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
            sp_parameters.Add("Day", request.Day, DbType.DateTime);
            sp_parameters.Add("TimeInit", request.TimeInit, DbType.String);
            sp_parameters.Add("TimeEnd", request.TimeEnd, DbType.String);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleTemp, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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

    public async Task<CommonActionResponse> RemoveDoctorAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("Id", Id, DbType.Int64);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleDoctor, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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

    public async Task<CommonActionResponse> RemoveScheduleTempAsync(long Id)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("Id", Id, DbType.Int64);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleTemp, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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

    public async Task<CommonActionResponse> UpdateScheduleDoctorAsync(UpdateScheduleDoctorRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("DoctorId", request.DoctorId, DbType.Int64);
            sp_parameters.Add("TimeInit", request.TimeInit, DbType.String);
            sp_parameters.Add("TimeEnd", request.TimeEnd, DbType.String);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleTemp, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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

    public async Task<CommonActionResponse> UpdateScheduleTempAsync(UpdateScheduleTempRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new DynamicParameters();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("Day", request.Day, DbType.DateTime);
            sp_parameters.Add("TimeInit", request.TimeInit, DbType.String);
            sp_parameters.Add("TimeEnd", request.TimeEnd, DbType.String);
            var result = await _storedProcRepository
            .Initialize(_spAdminLabScheduleTemp, sp_parameters)
            .Execute<CommonExecutionModel>();
            if(result!.Code != 200)
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
