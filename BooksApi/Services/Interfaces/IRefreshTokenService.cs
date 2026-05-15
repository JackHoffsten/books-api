namespace BooksApi.Services.Interfaces
{
  public interface IRefreshTokenService
  {
    string GenerateToken();
    string HashToken(string refreshToken);
    bool VerifyToken(string refreshToken, string hash);
    DateTime GetTokenExpiryDate();
  }
}