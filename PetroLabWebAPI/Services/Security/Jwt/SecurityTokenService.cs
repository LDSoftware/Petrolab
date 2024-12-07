using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PetroLabWebAPI.Security.Config;

namespace PetroLabWebAPI.Services.Security.Jwt;

public class SecurityTokenService(IOptions<JwtConfig> _jwtConfig) : ISecurityTokenService
{
    public string CreateUserToken(IdentityUser identityUser, string roleName)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtConfig.Value.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("Id", identityUser.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.UserName!),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserType",roleName),
                }),
            Expires = DateTime.UtcNow.AddHours(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}
