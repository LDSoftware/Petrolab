using Microsoft.AspNetCore.Identity;

namespace PetroLabWebAPI.Security.Entity;

  public class IdentityUser<TKey> where TKey : IEquatable<TKey>
    {

        [PersonalData]
        public virtual TKey? Id { get; set; }
        public virtual string UserName { get; set; } = string.Empty;
        public virtual string NormalizedUserName { get; set; } = string.Empty;

        [ProtectedPersonalData]
        public virtual string Email { get; set; } = string.Empty;
        public virtual string NormalizedEmail { get; set; } = string.Empty;

        [PersonalData]
        public virtual bool EmailConfirmed { get; set; }
        public virtual string PasswordHash { get; set; } = string.Empty;

        public virtual string SecurityStamp { get; set; } = string.Empty;
        public virtual string ConcurrencyStamp { get; set; } = string.Empty;

        [ProtectedPersonalData]
        public virtual string PhoneNumber { get; set; } = string.Empty;

        [PersonalData]
        public virtual bool PhoneNumberConfirmed { get; set; }

        [PersonalData]
        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTimeOffset? LockoutEnd { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }
        public virtual string Area { get; set; } = string.Empty;
        public virtual bool? UserPrincipal { get; set; } = null;
    }