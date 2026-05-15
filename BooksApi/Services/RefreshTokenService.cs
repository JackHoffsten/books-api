using System.Security.Cryptography;
using System.Text;
using BooksApi.Services.Interfaces;

namespace BooksApi.Services
{
  public class RefreshTokenService : IRefreshTokenService
  {
    private readonly IConfiguration _configuration;

    public RefreshTokenService(IConfiguration configuration)
    {
      _configuration = configuration;
    }
    public string GenerateToken()
    {
      var randomNumber = new byte[64];
      using var rng = RandomNumberGenerator.Create();
      rng.GetBytes(randomNumber);
      return Convert.ToBase64String(randomNumber);
    }

    public string HashToken(string refreshToken)
    {
      using var sha256 = SHA256.Create();
      var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
      return Convert.ToBase64String(hash);
    }

    public bool VerifyToken(string refreshToken, string hash)
    {
      return HashToken(refreshToken) == hash;
    }

    public DateTime GetTokenExpiryDate()
    {
      var expirationDays = int.Parse(_configuration["Jwt:RefreshTokenExpirationDays"] ?? "7");
      return DateTime.UtcNow.AddDays(expirationDays);
    }
  }
}