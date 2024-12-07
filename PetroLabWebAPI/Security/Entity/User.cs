using Microsoft.AspNetCore.Identity;

namespace PetroLabWebAPI.Security.Entity;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string MotherLastName { get; set; } = null!;
}
