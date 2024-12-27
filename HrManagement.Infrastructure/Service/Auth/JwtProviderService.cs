using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HrManagement.Application.Interfaces.Services;
using HrManagement.Domain.Entities.Identity;
using HrManagement.Domain.Shared;
using HrManagement.Domain.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HrManagement.Infrastructure.Service.Auth;

public class JwtProviderService(UserManager<AppUser> userManager, IOptions<JwtOption> jwtOption) : IJwtProviderService
{
    public async Task<TokenDto> CreateTokenAsync(AppUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var claims = GetClaims(user, roles);
        var authSigningKey = GetSigningKey(jwtOption.Value.Secret);
        var expires = DateTime.UtcNow.AddHours(3);

        var token = new JwtSecurityToken(
            issuer: jwtOption.Value.Issuer,
            audience: jwtOption.Value.Audience,
            notBefore: DateTime.UtcNow,
            expires: expires,
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), expires);
    }

    private List<Claim> GetClaims(AppUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private SymmetricSecurityKey GetSigningKey(string secret)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
}