using System.Data;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.ExecutionModel;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;

namespace PetroLabWebAPI.Services.Operation;

public class BranchSheduleService
(
    StoredProcRepository _storedProcRepository,
    IBranchService _branchService
) : IBranchSheduleService
{
    private const string _spAdminLabSchedule = "sp_AdminLabSchedule";
    private const string _spAdminLabScheduleTemp = "sp_AdminLabScheduleTemp";
    private const string _spAdminLabScheduleDoctor = "sp_AdminLabScheduleDoctor";

    public async Task<CommonActionResponse> AddScheduleDoctorAsync(CreateScheduleDoctorRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
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
                if (result!.Code != 200)
                {
                    throw new Exception(result.Message);
                }
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
            DynamicParameters sp_parameters = new();
            foreach (var item in request.ScheduleTemp)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "INS", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("Day", item.Day, DbType.DateTime);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                sp_parameters.Add("@IdDoctor", item.IdDoctor, DbType.Int64);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabScheduleTemp, sp_parameters)
                .Execute<CommonExecutionModel>();
                if (result!.Code != 200)
                {
                    throw new Exception(result.Message);
                }
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
            if (result!.Code != 200)
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
            if (result!.Code != 200)
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
            DynamicParameters sp_parameters = new();

            foreach (var item in request.Doctors)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "UPD", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("Id", item.Id, DbType.Int64);
                sp_parameters.Add("DoctorId", item.DoctorId, DbType.Int64);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabScheduleDoctor, sp_parameters)
                .Execute<CommonExecutionModel>();
                if (result!.Code != 200)
                {
                    throw new Exception(result.Message);
                }
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
            DynamicParameters sp_parameters = new();
            foreach (var item in request.ScheduleTemp)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "UPD", DbType.String);
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("Id", item.Id, DbType.Int64);
                sp_parameters.Add("Day", item.Day, DbType.DateTime);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                sp_parameters.Add("@IdDoctor", item.IdDoctor, DbType.Int64);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabScheduleTemp, sp_parameters)
                .Execute<CommonExecutionModel>();
                if (result!.Code != 200)
                {
                    throw new Exception(result.Message);
                }
            }
            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<CreateActionResponse> CreateBranchWithSchedule(CreateBranchWithScheduleRequest request)
    {
        try
        {
            var branchCreated = await _branchService.CreateAsync(request.BranchRequest);
            if (branchCreated.Code != 200)
            {
                throw new Exception(branchCreated.Message);
            }
            var scheduleCreated = await AddScheduleAsync(
                new(branchCreated.Id,
                Schedule: request.ScheduleRequest.Schedule,
                ScheduleTemp: request.ScheduleRequest.ScheduleTemp,
                Doctors: request.ScheduleRequest.Doctors));

            return new(branchCreated.Id);
        }
        catch (Exception ex)
        {
            return new(0, 500, ex.Message);
        }
    }

    public async Task<CommonActionResponse> UpdateScheduleAsync(UpdateBranchScheduleRequest request)
    {
        try
        {
            var responseUpdateSchduleTemp = await UpdateScheduleTempAsync(
                new(request.IdLabBranch, request.ScheduleTemp));

            var responseUpdateSchduleDoctor = await UpdateScheduleDoctorAsync(
                new(request.IdLabBranch, request.Doctors));

            return new();
        }
        catch (Exception ex)
        {
            return new(500, ex.Message);
        }
    }

    public async Task<GetBranchScheduleResponse> GetBranchScheduleAsync(long IdBranch)
    {
        try
        {
            var branch = await _branchService.GetBranchByIdAsync(IdBranch);
            if (branch.ServiceStatus.Code != 200)
            {
                throw new Exception(branch.ServiceStatus.Message);
            }

            DynamicParameters sp_parameters = new();

            sp_parameters.Add("Action", "SEL", DbType.String);
            sp_parameters.Add("@IdLabBranch", IdBranch, DbType.Int64);

            var branchSchedule = await _storedProcRepository.Initialize(_spAdminLabSchedule, sp_parameters)
                .ReturnCollection<LabBranchSchedule>();

            var branchScheduleTemp = await _storedProcRepository.Initialize(_spAdminLabScheduleTemp, sp_parameters)
                .ReturnCollection<LabBranchScheduleTemp>();

            var branchScheduleDoctor = await _storedProcRepository.Initialize(_spAdminLabScheduleDoctor, sp_parameters)
                .ReturnCollection<LabBranchScheduleDoctor>();

            return new(
                new(
                    Branch: branch.DataResult,
                    BranchSchedule: branchSchedule!.Select(x => new BranchScheduleDtoItem(x.Id, x.IdLabBranch, x.DayOfWeek, x.TimeInit, x.TimeEnd)).ToList(),
                    BranchScheduleTemp: branchScheduleTemp!.Select(x => new BranchScheduleTempDtoItem(x.Id, x.IdLabBranch, x.Day, x.TimeInit, x.TimeEnd, x.IdDoctor)).ToList(),
                    BranchScheduleDoctor: branchScheduleDoctor!.Select(x => new BranchScheduleDoctorDtoItem(x.Id, x.IdLabBranch, x.TimeInit, x.TimeEnd, x.DoctorId)).ToList()
                ),
                new()
            );
        }
        catch (Exception)
        {
            return new(null!, new(500, "Error"));
        }
    }
}
