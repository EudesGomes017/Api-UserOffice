using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private const string NameAlias = "Name";
    private const string RoleAlias = "Role";

    private readonly double _tokenLifetimeInMinutes;
    private readonly string _securityKey;

    public TokenController(double tokenLifetimeInMinutes, string SecurityKey)
    {
        _tokenLifetimeInMinutes = tokenLifetimeInMinutes;
        _securityKey = SecurityKey;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
                    new Claim(EmailAlias, user.Email),//  para recuperar o email para alterar a senha
                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Name, user.Name),
                   new Claim(ClaimTypes.Role, user.Role)

            };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tokenLifetimeInMinutes),
            SigningCredentials = new SigningCredentials(SimtricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);

    }

    public ClaimsPrincipal ValidationToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parameterValidation = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimtricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,

        };

        var claims = tokenHandler.ValidateToken(token, parameterValidation, out _);

        return claims;
    }

    public string RecoverEmail(string token)
    {
        var claims = ValidationToken(token);

        return claims.FindFirst(EmailAlias).Value;
    }

    public SymmetricSecurityKey SimtricKey()
    {
        var symmetricKey = Convert.FromBase64String(_securityKey);
        return new SymmetricSecurityKey(symmetricKey);
    }
}

