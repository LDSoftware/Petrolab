using PetroLabWebAPI.ServiceDto.Security.Login;

namespace PetroLabWebAPI.Services.Security.Login;

public interface IUserLoginService
{
    Task<LoginResponse> Login(LoginRequest request);
}
