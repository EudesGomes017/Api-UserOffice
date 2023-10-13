using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Token;

public class TokenController
{
    private const string EmailAlias = "Email";
    private const string NameAlias = "Name";
    private const string RoleAlias = "Role";

    private readonly double _tempoDeVidaDoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double tempoDeVidaDoTokenEmMinutos, string chaveDeSeguranca)
    {
        _tempoDeVidaDoTokenEmMinutos = tempoDeVidaDoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(User user)
    {
        var claims = new List<Claim>
        {

                   new Claim(ClaimTypes.Email, user.Email),
                   new Claim(ClaimTypes.Name, user.Name),
                   new Claim(ClaimTypes.Role, user.Role)

                    //new Claim(EmailAlias, user),
                    //new Claim(NameAlias, user),
                    //new Claim(RoleAlias, user)

            };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaDoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SimtricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);

    }

    public void ValidationToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametroValidation = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimtricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,

        };

        tokenHandler.ValidateToken(token, parametroValidation, out _);

    }

    public SymmetricSecurityKey SimtricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }
}

