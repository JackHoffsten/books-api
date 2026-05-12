namespace BooksApi.Services.Interfaces
{
  public interface IJwtTokenService
  {
    string GenerateToken(int userId, string username);
  }
}
