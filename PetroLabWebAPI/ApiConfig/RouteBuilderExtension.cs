using Microsoft.AspNetCore.Mvc;
using PetroLabWebAPI.ServiceDto.Branch.Request;
using PetroLabWebAPI.ServiceDto.Branch.Response;
using PetroLabWebAPI.ServiceDto.Common;
using PetroLabWebAPI.ServiceDto.Customer.Request;
using PetroLabWebAPI.ServiceDto.Customer.Response;
using PetroLabWebAPI.ServiceDto.Doctor.Request;
using PetroLabWebAPI.ServiceDto.Doctor.Response;
using PetroLabWebAPI.ServiceDto.LabStudio.Request;
using PetroLabWebAPI.ServiceDto.LabStudio.Response;
using PetroLabWebAPI.ServiceDto.Security.Login;
using PetroLabWebAPI.ServiceDto.Security.RoleManagment;
using PetroLabWebAPI.ServiceDto.Security.UserManagment;
using PetroLabWebAPI.Services;
using PetroLabWebAPI.Services.Operation;
using PetroLabWebAPI.Services.Security.Login;
using PetroLabWebAPI.Services.Security.RoleManagment;
using PetroLabWebAPI.Services.Security.UserManagment;
using PetroLabWebAPI.ValidationRules.Commands;

namespace PetroLabWebAPI.ApiConfig;

public static class RouteBuilderExtension
{
    public static RouteGroupBuilder MapBranchApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createbranch", async (IBranchService _service,
            [FromBody] CreateBranchRequest request) =>
        {
            var response = await _service.CreateAsync(request);
            return response;
        }).WithName("CreateBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapPut("/updatebranch", async (IBranchService _service,
            [FromBody] UpdateBranchRequest request) =>
        {
            var response = await _service.UpdateAsync(request);
            return response;
        }).WithName("UpdateBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletebranch", async (IBranchService _service,
            [FromBody] DeleteBranchRequest request) =>
        {
            var response = await _service.DeleteAsync(request);
            return response;
        }).WithName("DeleteBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getbranch", async (IBranchService _service) =>
        {
            var response = await _service.GetBranchAsync();
            return response;
        }).WithName("GetBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetBranchResponse>();

        group.MapGet("/getbranchbyid/{id}", async (long id, IBranchService _service) =>
        {
            var response = await _service.GetBranchByIdAsync(id);
            return response;
        }).WithName("GetBranchById")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetBranchResponse>();

        group.MapPost("/createnewdoctortobranch", async (InsertNewDoctorCommand _service,
            [FromBody] CreateDoctorBranchRequest request) =>
        {
            _service.Request = request;
            var response = await _service.ExecuteAsync();
            return response;
        }).WithName("CreateNewDoctorToBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapDelete("/deletenewdoctortobranch", async (IBranchService _service,
            [FromBody] DeleteDoctorBranchRequest request) =>
        {
            var response = await _service.DeleteDoctorToBranch(request);
            return response;
        }).WithName("DeleteNewDoctorToBranch")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        return group;
    }

    public static RouteGroupBuilder MapScheduleBranchApi(this RouteGroupBuilder group)
    {

        group.MapPost("/createbranchwithschedule", async (IBranchSheduleService _service,
            [FromBody] CreateBranchWithScheduleRequest request) =>
        {
            var response = await _service.CreateBranchWithSchedule(request);
            return response;
        }).WithName("CreateBranchWithSchedule").WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapPost("/addschedulebranch", async (IBranchSheduleService _service,
            [FromBody] CreateBranchScheduleRequest request) =>
        {
            var response = await _service.AddScheduleAsync(request);
            return response;
        }).WithName("AddSchedule")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPost("/addschedulebranchtemp", async (IBranchSheduleService _service,
            [FromBody] CreateScheduleTempRequest request) =>
        {
            var response = await _service.AddScheduleTempAsync(request);
            return response;
        }).WithName("AddScheduleTemp")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPost("/addschedulebranchdoctor", async (IBranchSheduleService _service,
            [FromBody] CreateScheduleDoctorRequest request) =>
        {
            var response = await _service.AddScheduleDoctorAsync(request);
            return response;
        }).WithName("AddDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPut("/updateschedulebranchtemp", async (IBranchSheduleService _service,
            [FromBody] UpdateScheduleTempRequest request) =>
        {
            var response = await _service.UpdateScheduleTempAsync(request);
            return response;
        }).WithName("UpdateScheduleTemp")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPut("/updateschedulebranchdoctor", async (IBranchSheduleService _service,
            [FromBody] UpdateScheduleDoctorRequest request) =>
        {
            var response = await _service.UpdateScheduleDoctorAsync(request);
            return response;
        }).WithName("UpdateScheduleDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        return group;
    }

    public static RouteGroupBuilder MapCustomerApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createcustomer", async (ICustomerService _service,
            [FromBody] CreateCustomerRequest request) =>
        {
            var response = await _service.CreateAsync(request);
            return response;
        }).WithName("CreateCustomer")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapPut("/updatecustomer", async (ICustomerService _service,
            [FromBody] UpdateCustomerRequest request) =>
        {
            var response = await _service.UpdateAsync(request);
            return response;
        }).WithName("UpdateCustomer")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletecustomer", async (ICustomerService _service,
            [FromBody] DeleteCustomerRequest request) =>
        {
            var response = await _service.DeleteAsync(request);
            return response;
        }).WithName("DeleteCustomer")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getcustomer", async (ICustomerService _service) =>
        {
            var response = await _service.GetCustomerAsync();
            return response;
        }).WithName("GetCustomer")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetCustomerResponse>();

        group.MapGet("/getcustomerbyid/{id}", async (long id, ICustomerService _service) =>
        {
            var response = await _service.GetCustomerByIdAsync(id);
            return response;
        }).WithName("GetCustomerById")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetCustomerByIdResponse>();

        return group;
    }

    public static RouteGroupBuilder MapDoctorApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createdoctor", async (IDoctorService _service,
            [FromBody] CreateDoctorRequest request) =>
        {
            var response = await _service.CreateAsync(request);
            return response;
        }).WithName("CreateDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapPut("/updatedoctor", async (IDoctorService _service,
            [FromBody] UpdateDoctorRequest request) =>
        {
            var response = await _service.UpdateAsync(request);
            return response;
        }).WithName("UpdateDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletedoctor", async (IDoctorService _service,
            [FromBody] DeleteDoctorRequest request) =>
        {
            var response = await _service.DeleteAsync(request);
            return response;
        }).WithName("DeleteDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getdoctor/{idBranch}", async (long idBranch, IDoctorService _service) =>
        {
            var response = await _service.GetDoctorAsync(idBranch);
            return response;
        }).WithName("GetDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetDoctorResponse>();

        group.MapGet("/getdoctorbyid/{id}", async (long id, IDoctorService _service) =>
        {
            var response = await _service.GetDoctorByIdAsync(id);
            return response;
        }).WithName("GetDoctorById")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetDoctorByIdResponse>();

        group.MapPost("/insertlabstudioondoctor", async (InsertNewLabStudioCommand _service,
            [FromBody] ManageDoctorLabStudioRequest request) =>
        {
            _service.Request = request;
            var response = await _service.ExecuteAsync();
            return response;
        }).WithName("InsertLabStudioOnDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletelabstudioondoctor", async (IDoctorService _service,
            [FromBody] ManageDoctorLabStudioRequest request) =>
        {
            var response = await _service.DeleteDoctorLabStudio(request);
            return response;
        }).WithName("DeleteLabStudioOnDoctor")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        return group;
    }

    public static RouteGroupBuilder MapLabStudioApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createlabstudio", async (ILabStudioService _service,
            [FromBody] CreateLabStudioRequest request) =>
        {
            var response = await _service.CreateAsync(request);
            return response;
        }).WithName("CreateLabStudio")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CreateActionResponse>();

        group.MapPut("/updatelabstudio", async (ILabStudioService _service,
            [FromBody] UpdateLabStudioRequest request) =>
        {
            var response = await _service.UpdateAsync(request);
            return response;
        }).WithName("UpdateLabStudio")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletelabstudio", async (ILabStudioService _service,
            [FromBody] DeleteLabStudioRequest request) =>
        {
            var response = await _service.DeleteAsync(request);
            return response;
        }).WithName("DeleteLabStudio")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getlabstudio", async (ILabStudioService _service) =>
        {
            var response = await _service.GetLabStudioAsync();
            return response;
        }).WithName("GetLabStudio")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetLabStudioResponse>();

        group.MapGet("/getlabstudiobyid/{id}", async (long id, ILabStudioService _service) =>
        {
            var response = await _service.GetLabStudioByIdAsync(id);
            return response;
        }).WithName("GetLabStudioById")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetLabStudioByIdResponse>();


        group.MapGet("/getlabstudiospeciality", async (ILabStudioService _service) =>
        {
            var response = await _service.GetLabStudioBySpecialtyAsync();
            return response;
        }).WithName("GetLabStudioBySpecialtyAsync")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetLabSpecialtyResponse>();

        group.MapGet("/getlabstudiospecialitygamas", async (ILabStudioService _service) =>
        {
            var response = await _service.GetLabSpecialityGamasAsync();
            return response;
        }).WithName("GetLabSpecialityGamas")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetLabSpecialityGamasResponse>();

        return group;
    }

    public static RouteGroupBuilder MapUserLoginApi(this RouteGroupBuilder group)
    {
        group.MapPost("/login", async (IUserLoginService _service,
            [FromBody] LoginRequest request) =>
        {
            var response = await _service.Login(request);
            return response;
        }).WithName("UserLogin")
        .WithOpenApi()
        .Produces<LoginResponse>();

        return group;
    }

    public static RouteGroupBuilder MapRoleManagmentApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createrole", async (IRoleManagmentService _service,
            [FromBody] CreateRoleRequest request) =>
        {
            var response = await _service.CreateRole(request);
            return response;
        }).WithName("CreateRole")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPut("/updaterole", async (IRoleManagmentService _service,
            [FromBody] UpdateRoleRequest request) =>
        {
            var response = await _service.UpdateRole(request);
            return response;
        }).WithName("UpdateRole")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deleterole", async (IRoleManagmentService _service,
            [FromBody] DeleteRoleRequest request) =>
        {
            var response = await _service.DeleteRole(request);
            return response;
        }).WithName("DeleteRole")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getroles", async (IRoleManagmentService _service) =>
        {
            var response = await _service.GetRoles();
            return response;
        }).WithName("GetRoles")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetRoleResponse>();

        return group;
    }

    public static RouteGroupBuilder MapUserManagmentApi(this RouteGroupBuilder group)
    {
        group.MapPost("/createuser", async (IUserManagmentService _service,
            [FromBody] CreateUserRequest request) =>
        {
            var response = await _service.CreateUser(request);
            return response;
        }).WithName("CreateUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPut("/updateuser", async (IUserManagmentService _service,
            [FromBody] UpdateUserRequest request) =>
        {
            var response = await _service.UpdateUser(request);
            return response;
        }).WithName("UpdateUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deleteuser", async (IUserManagmentService _service,
            [FromBody] DeleteUserRequest request) =>
        {
            var response = await _service.DeleteUser(request.UserId);
            return response;
        }).WithName("DeleteUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPatch("/lockunlockuser", async (IUserManagmentService _service,
            [FromBody] LockUnlockUserRequest request) =>
        {
            var response = await _service.LockUnlockUser(request);
            return response;
        }).WithName("LockUnlockUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapGet("/getusersbyid/{userid}", async (string userid, IUserManagmentService _service) =>
        {
            var response = await _service.GetUsersById(userid);
            return response;
        }).WithName("GetUsersById")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetUserDetailResponse>();

        group.MapGet("/getusers", async (IUserManagmentService _service) =>
        {
            var response = await _service.GetUsers();
            return response;
        }).WithName("GetUsers")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<GetUsersResponse>();

        group.MapPost("/insertnewbranchonuser", async (InsertNewBrachCommand _service,
            [FromBody] ManageUserBranchsRequest request) =>
        {
            _service.Request = request;
            var response = await _service.ExecuteAsync();
            return response;
        }).WithName("InsertNewBranchOnUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapDelete("/deletebranchonuser", async (IUserManagmentService _service,
            [FromBody] DelteUserBranchsRequest request) =>
        {
            var response = await _service.DeleteBranchOnUser(request);
            return response;
        }).WithName("DeleteBranchOnUser")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        group.MapPut("/setbranchtoprincipal", async (IUserManagmentService _service,
            [FromBody] UpdateUserBranchToPrincipalRequest request) =>
        {
            var response = await _service.SetBranchToPrincipal(request);
            return response;
        }).WithName("SetBranchToPrincipal")
        .WithOpenApi()
        .RequireAuthorization()
        .Produces<CommonActionResponse>();

        return group;
    }
}
