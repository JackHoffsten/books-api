using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BooksApi.Services.Interfaces;
using System.Security.Cryptography;
using BooksApi.Models;

namespace BooksApi.Services
{
  public class JwtTokenService : IJwtTokenService
  {
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
      var secretKey = _configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
      var issuer = _configuration["Jwt:Issuer"] ?? "BooksApi";
      var audience = _configuration["Jwt:Audience"] ?? "BooksApi";
      var expirationMinutes = int.Parse(_configuration["Jwt:AccessTokenExpirationMinutes"] ?? "60");

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
      };

      var token = new JwtSecurityToken(
          issuer: issuer,
          audience: audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
          signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
