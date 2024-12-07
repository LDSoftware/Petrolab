using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PetroLabWebAPI.Security.Entity;

public class SecurityDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
        : base(options)
    {
    }
}