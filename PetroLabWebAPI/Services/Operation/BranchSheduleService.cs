using System.Data;
using Dapper;
using Microsoft.Identity.Client;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.ExecutionModel;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Schedule.Response;
using PetroLabWebAPI.Services.Helpers;

namespace PetroLabWebAPI.Services.Operation;

public class BranchSheduleService
(
    StoredProcRepository _storedProcRepository,
    IBranchService _branchService,
    IScheduleGeneratorService _scheduleGeneratorService,
    ICustomerScheduleService _customerScheduleService,
    ILabStudioService _labStudioService,
    IDoctorService _doctorService
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
            bool deleteConfig = await DeleteBranchConfiguration(request.IdLabBranch);

            if(!deleteConfig)
            {
                throw new Exception("Error al eliminar configuración de la sucursal");
            }

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
                sp_parameters.Add("IdDoctor", item.IdDoctor, DbType.Int64);
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
            DynamicParameters sp_parameters = new();
            foreach (var item in request.Schedule)
            {
                sp_parameters = new DynamicParameters();
                sp_parameters.Add("Action", "UPD", DbType.String);
                sp_parameters.Add("Id", item.Id, DbType.Int64);                
                sp_parameters.Add("IdLabBranch", request.IdLabBranch, DbType.Int64);
                sp_parameters.Add("DayOfWeek", item.DayOfWeek, DbType.String);
                sp_parameters.Add("TimeInit", item.TimeInit, DbType.String);
                sp_parameters.Add("TimeEnd", item.TimeEnd, DbType.String);
                var result = await _storedProcRepository
                .Initialize(_spAdminLabSchedule, sp_parameters)
                .Execute<CommonExecutionModel>();
            }

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
            var doctors = await _doctorService.GetDoctorAsync(IdBranch);
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
                    BranchScheduleDoctor: branchScheduleDoctor!
                    .Select(x => new BranchScheduleDoctorDtoItem(
                    x.Id,
                    x.IdLabBranch,
                    x.TimeInit,
                    x.TimeEnd,
                    x.DoctorId,
                    x.DoctorName
                    )).ToList()
                ),
                new()
            );
        }
        catch (Exception ex)
        {
            return new(null!, new(500, ex.Message));
        }
    }

    public async Task<GetAllBranchScheduleResponse> GetAllBranchScheduleAsync()
    {
        try
        {
            var branches = await _branchService.GetBranchAsync();
            if (branches.ServiceStatus.Code != 200)
            {
                throw new Exception(branches.ServiceStatus.Message);
            }

            List<GetBranchScheduleDtoItem>? _dataResult = new();

            foreach (var item in branches.DataResult!)
            {
                var branchSchedule = await GetBranchScheduleAsync(item.Id);
                if (branchSchedule.ServiceStatus.Code != 200)
                {
                    throw new Exception(branchSchedule.ServiceStatus.Message);
                }
                _dataResult.Add(branchSchedule.DataResult!);
            }

            return new(_dataResult, new());
        }
        catch (Exception ex)
        {
            return new(null!, new(500, ex.Message));
        }
    }

    public async Task<GetLabCustomerSchedulerDateTimeResponse> GetLabCustomerSchedulerDateTimeAsync(GetLabCustomerSchedulerDateTimeRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
            sp_parameters.Add("Action", "SEL", DbType.String);
            sp_parameters.Add("@IdLabBranch", request.IdBranch, DbType.Int64);
            var branchSchedule = await _storedProcRepository.Initialize(_spAdminLabSchedule, sp_parameters)
                .ReturnCollection<LabBranchSchedule>();

            var scheduleMonth = _scheduleGeneratorService.GenerateSchedule(request.startDate, request.endDate);
            var labStudio = await _labStudioService.GetLabStudioByIdAsync(request.IdLabStudio);
            if (labStudio.ServiceStatus.Code != 200)
            {
                throw new Exception(labStudio.ServiceStatus.Message);
            }

            var branchScheduleTemp = await _storedProcRepository.Initialize(_spAdminLabScheduleTemp, sp_parameters)
                .ReturnCollection<LabBranchScheduleTemp>();

            List<DateTime> schedule = new();
            foreach (var item in scheduleMonth!)
            {
                bool isTempDay = branchScheduleTemp!.Any(x => x.Day == item);
                var scheduleTemp = branchScheduleTemp!.FirstOrDefault(x => x.Day == item);
                foreach (var element in branchSchedule!)
                {
                    if (isTempDay)
                    {
                        if (element.DayOfWeek == _scheduleGeneratorService.GetDayOfWeek(item.DayOfWeek.ToString()))
                        {
                            var scheduleHour = _scheduleGeneratorService
                            .GenerateScheduleHour(item, scheduleTemp!.TimeInit, scheduleTemp.TimeEnd, labStudio.DataResult!.Duration);
                            schedule.AddRange(scheduleHour);
                        }
                    }
                    else
                    {
                        if (element.DayOfWeek == _scheduleGeneratorService.GetDayOfWeek(item.DayOfWeek.ToString()))
                        {
                            var scheduleHour = _scheduleGeneratorService
                            .GenerateScheduleHour(item, element.TimeInit, element.TimeEnd, labStudio.DataResult!.Duration);
                            schedule.AddRange(scheduleHour);
                        }
                    }
                }
            }

            var customerSchedule = await _customerScheduleService
                .GetLabCustomerScheduleResponseAsync(new LabCustomerScheduleFilterRequest(
                    StarDate: scheduleMonth.First(),
                    EndDate: scheduleMonth.Last(),
                    IdLabStudio: request.IdLabStudio,
                    IdBranch: request.IdBranch,
                    Cancel: false));

            IList<ScheduleDateDtoItem>? _dataResult =
                scheduleMonth
                .Select(x => new ScheduleDateDtoItem(x.ToString("yyyy-MM-dd"),
                schedule.Where(r => r.Day.Equals(x.Day))
                .Select(s => new ScheduleHourDtoItem(s.ToString("HH:mm:ss"), false)).ToList()))
                .ToList();

            if (customerSchedule.ServiceStatus.Code == 200 && customerSchedule.DataResult!.Any())
            {
                foreach (var item in customerSchedule.DataResult!)
                {
                    ITimeRangeService<DateTime> timeRangeService = new TimeRangeService(item.StarDate, item.EndDate);
                    foreach (var element in _dataResult)
                    {
                        foreach (var hour in element.Hours)
                        {
                            DateTime date = DateTime.Parse($"{element.Day} {hour.Hour}");
                            if (timeRangeService.Includes(date))
                            {
                                hour.IsReserved = true;
                            }
                        }
                    }
                }
            }

            return new(_dataResult.Where(d => d.Hours.Count > 0).ToList(), new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    private async Task<bool> DeleteBranchConfiguration(long IdBranch)
    {
        try
        {
            DynamicParameters sp_parameters = new();

            // Eliminar Schedule
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("IdLabBranch", IdBranch, DbType.Int64);
            var resultDelSchedule = await _storedProcRepository
            .Initialize(_spAdminLabSchedule, sp_parameters)
            .Execute<CommonExecutionModel>();

            // Eliminar Doctor
            sp_parameters = new();            
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("IdLabBranch", IdBranch, DbType.Int64);
            var resultDelDoc = await _storedProcRepository
            .Initialize(_spAdminLabScheduleDoctor, sp_parameters)
            .Execute<CommonExecutionModel>();

            // ELiminar schedule temporal
            sp_parameters = new();            
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("IdLabBranch", IdBranch, DbType.Int64);
            var resultDelTemp = await _storedProcRepository
            .Initialize(_spAdminLabScheduleTemp, sp_parameters)
            .Execute<CommonExecutionModel>();
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

}
