using System.Data;
using Dapper;
using PetroLabWebAPI.Data.Domain;
using PetroLabWebAPI.Data.ExecutionModel;
using PetroLabWebAPI.Data.Repository;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Schedule.Request;
using PetroLabWebAPI.ServiceDto.Schedule.Response;

namespace PetroLabWebAPI.Services.Operation;

public class CustomerScheduleService
(
    StoredProcRepository _spAdminCustomerSchedule
) : ICustomerScheduleService
{
    private const string _spName = "sp_AdminCustomerSchedule";
    public async Task<CommonActionResponse> CancelCustomerScheduleAsync(CancelCustomerScheduleRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
            sp_parameters.Add("Action", "DEL", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            var result = await _spAdminCustomerSchedule
            .Initialize(_spName, sp_parameters)
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

    public async Task<CommonActionResponse> CreateCustomerScheduleAsync(CreateCustomerScheduleRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
            sp_parameters.Add("Action", "INS", DbType.String);
            sp_parameters.Add("Name", request.Name, DbType.String);
            sp_parameters.Add("LastName", request.LastName, DbType.String);
            sp_parameters.Add("MotherLastName", request.MotherLastName, DbType.String);
            sp_parameters.Add("CellPhone", request.CellPhone, DbType.String);
            sp_parameters.Add("Phone", request.Phone, DbType.String);
            sp_parameters.Add("Email", request.Email, DbType.String);
            sp_parameters.Add("StarDate", request.StarDate, DbType.DateTime);
            sp_parameters.Add("EndDate", request.EndDate, DbType.DateTime);
            sp_parameters.Add("IdLabStudio", request.IdLabStudio, DbType.Int64);
            sp_parameters.Add("IdBranch", request.IdBranch, DbType.Int64);
            sp_parameters.Add("Comments", request.Comments, DbType.String);
            sp_parameters.Add("ProofOfPayment", request.ProofOfPayment, DbType.String);
            var result = await _spAdminCustomerSchedule
            .Initialize(_spName, sp_parameters)
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

    public async Task<GetLabCustomerScheduleResponse> GetLabCustomerScheduleResponseAsync(LabCustomerScheduleFilterRequest filterRequest)
    {
        try
        {
            List<LabCustomerSchedule> dataResult = new();
            DynamicParameters sp_parameters = new();
            sp_parameters.Add("Action", "SEL", DbType.String);
            sp_parameters.Add("FilterStartDate", filterRequest.StarDate, DbType.DateTime);
            sp_parameters.Add("FilterEndDate", filterRequest.EndDate, DbType.DateTime);

            var result = await _spAdminCustomerSchedule
            .Initialize(_spName, sp_parameters)
            .ReturnCollection<LabCustomerSchedule>();

            dataResult.AddRange(result!);

            if (!filterRequest.IdBranch.Equals(0) && result!.Any())
            {
                dataResult.Clear();
                dataResult.AddRange(result!.Where(x => x.IdBranch.Equals(filterRequest.IdBranch)));
            }

            if (!filterRequest.IdLabStudio.Equals(0) && result!.Any())
            {
                dataResult.Clear();
                dataResult.AddRange(result!.Where(x => x.IdLabStudio.Equals(filterRequest.IdLabStudio)));
            }

            if (filterRequest.Cancel && result!.Any())
            {
                dataResult.Clear();
                dataResult.AddRange(result!.Where(x => x.Cancel.Equals(true)));
            }

            return new(dataResult.Select(x => new LabCustomerScheduleDtoItem(
                x.Id,
                x.Name,
                x.LastName,
                x.MotherLastName,
                x.CellPhone,
                x.Phone,
                x.Email,
                x.StarDate,
                x.EndDate,
                x.IdLabStudio,
                x.IdBranch,
                x.Comments,
                x.ProofOfPayment,
                x.Cancel,
                x.CancelDate)).ToList(),
            new());
        }
        catch (Exception ex)
        {
            return new(null, new(500, ex.Message));
        }
    }

    public async Task<CommonActionResponse> UpdateCustomerScheduleAsync(UpdateCustomerScheduleRequest request)
    {
        try
        {
            DynamicParameters sp_parameters = new();
            sp_parameters.Add("Action", "UPD", DbType.String);
            sp_parameters.Add("Id", request.Id, DbType.Int64);
            sp_parameters.Add("Name", request.Name, DbType.String);
            sp_parameters.Add("LastName", request.LastName, DbType.String);
            sp_parameters.Add("MotherLastName", request.MotherLastName, DbType.String);
            sp_parameters.Add("CellPhone", request.CellPhone, DbType.String);
            sp_parameters.Add("Phone", request.Phone, DbType.String);
            sp_parameters.Add("Email", request.Email, DbType.String);
            sp_parameters.Add("StarDate", request.StarDate, DbType.DateTime);
            sp_parameters.Add("EndDate", request.EndDate, DbType.DateTime);
            sp_parameters.Add("IdLabStudio", request.IdLabStudio, DbType.Int64);
            sp_parameters.Add("IdBranch", request.IdBranch, DbType.Int64);
            sp_parameters.Add("Comments", request.Comments, DbType.String);
            sp_parameters.Add("ProofOfPayment", request.ProofOfPayment, DbType.String);
            var result = await _spAdminCustomerSchedule
            .Initialize(_spName, sp_parameters)
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
}
