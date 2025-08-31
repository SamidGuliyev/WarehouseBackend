using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Infrastructure.Providers.Services;

public sealed class TokenService(IConfiguration config)
{
    public string GenerateToken(User user)
    {
        var subject = new ClaimsIdentity([
            new Claim(ClaimTypes.Sid, Guid.CreateVersion7().ToString()),
        new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
    ]);
        var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:SecretKey")!)), SecurityAlgorithms.HmacSha256);


        var descriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = signinCredentials,
            Audience = config.GetValue<string>("Jwt:Audience"),
            Issuer = config.GetValue<string>("Jwt:Issuer"),
        };

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    public ClaimsPrincipal? ParseJwtToken(string token)
    {
        var principal = new JwtSecurityTokenHandler()
            .ValidateToken(
                token,
                new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:SecretKey")!)),
                    ValidAudience = config.GetValue<string>("Jwt:Audience")!,
                    ValidIssuer = config.GetValue<string>("Jwt:Issuer")!,
                },
                out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token is invalid.");
        }

        return principal;
    }

}

