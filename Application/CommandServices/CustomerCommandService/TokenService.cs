using System.Security.Claims;
using System.Text;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


namespace Application.CommandServices.CustomerCommandService;

public class TokenService: ITokenService
{
    private readonly IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(Customer customer)
    {
        var secret = _configuration["AppSettings:Secret"];
        if (string.IsNullOrEmpty(secret))
            throw new ArgumentNullException("La configuración 'AppSettings:Secret' no puede ser nula o una cadena vacía");

        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Correo)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JsonWebTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }

    public async Task<int?> ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return null;
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
        try
        {
            var tokenValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Expiration without delay
                ClockSkew = TimeSpan.Zero
            });

            var jwtToken = (JsonWebToken)tokenValidationResult.SecurityToken;
            var userId = int.Parse(jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value);
            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}