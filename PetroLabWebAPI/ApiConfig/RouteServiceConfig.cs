namespace PetroLabWebAPI.ApiConfig;

    public static class RouteServiceConfig
    {
        public static void ConfigureApi(this WebApplication app)
        {
            app.MapGroup("/api/v1")
                .MapBranchApi()
                .WithTags("Branch");

            app.MapGroup("/api/v1")
                .MapCustomerApi()
                .WithTags("Customer");

            app.MapGroup("/api/v1")
                .MapDoctorApi()
                .WithTags("Doctor");

            app.MapGroup("/api/v1")
                .MapLabStudioApi()
                .WithTags("LabStudio");

            app.MapGroup("/api/v1")
                .MapUserLoginApi()
                .WithTags("UserLogin");

            app.MapGroup("/api/v1")
                .MapRoleManagmentApi()
                .WithTags("RoleManagment");

            app.MapGroup("/api/v1")
                .MapUserManagmentApi()
                .WithTags("UserManagment");
        }
    }
